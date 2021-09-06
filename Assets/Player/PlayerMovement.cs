using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Permissions;
using Interfaces;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public static int
        LOOK_DIRECTION_LEFT = -1, LOOK_DIRECTION_RIGHT = 1; // -1 is left, 1 is right, 0 is nothing - 0 is edge case.

    [SerializeField] private Animator animator;
    [SerializeField] public Collider2D topCollider;
    [SerializeField] public Collider2D sideCollider;
    [SerializeField] public Collider2D bottomCollider;

    [SerializeField] private float moveSpeed;
    public int damageLevel;
    public List<AbilityController> abilities;

    [HideInInspector] public Dictionary<string, int> upgradeLevels;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public PlayerMain main;
    [HideInInspector] public Rigidbody2D body;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canTurn;
    [HideInInspector] public bool canInteract;
    [HideInInspector] public bool isInAir;
    [HideInInspector] public bool isShifted;
    [HideInInspector] public bool isFalling;
    [HideInInspector] public bool canBashDownwards;
    [HideInInspector] public IInteractable interactable;

    private float timeSinceLastCarry;
    private int lookDirection;
    private Vector3 lastSafePosition;

    public static readonly string walkString = "Walk";
    public static readonly string jumpString = "Jump";
    public static readonly string diveString = "Dive";
    public static readonly string chargeString = "Charge";
    public static readonly string fallTrigger = "FallTrigger";
    public static readonly string lowerWingsTrigger = "LowerWings";
    public static readonly string doubleJumpString = "DoubleJump";
    public static readonly string ascendantString = "SaveSpot";
    public static readonly string filePath = "abfpth";


    private void Awake() {
        isInAir = false;
        canBashDownwards = false;
        isFalling = false;
        body = GetComponent<Rigidbody2D>();
        main = GetComponent<PlayerMain>();
        canMove = true;
        canTurn = true;
        canAttack = true;
        input = new PlayerInput();
        input.Enable();
        if (GameManager.playerMovement == null) {
            GameManager.playerMain = main;
            GameManager.playerMovement = this;
            GameManager.player = this.gameObject;
        } else {
            Destroy(this.gameObject);
            return;
        }

        input.Player.Move.performed += context => {
            float hInput = context.action.ReadValue<Vector2>().x;
            if (hInput > 0) lookDirection = 1;
            else if (hInput < 0) lookDirection = -1;
            // else lookDirection = 1;
        };
        input.Player.Move.canceled += context => { lookDirection = 0; };
        input.Player.Shift.started += context => { isShifted = true; };
        input.Player.Shift.canceled += context => { isShifted = false; };
        input.General.Interact.started += context => {
            if (canInteract && !isInAir) {
                interactable?.Interact();
            }
        };

        DontDestroyOnLoad(gameObject);
        // LoadLevels();
        upgradeLevels = new Dictionary<string, int>();
    }


    private void Update() {
        if (canMove) {
            if (!isInAir) {
                if (lookDirection > 0)
                    SetAnimationBool(walkString, true);
                else if (lookDirection < 0)
                    SetAnimationBool(walkString, true);
                else
                    SetAnimationBool(walkString, false);
            }
        }

        if (isInAir && !isFalling) {
            isFalling = true;
            SetAnimationTrigger(fallTrigger);
        }

        if (canTurn) {
            SetLookDirection(lookDirection);
            if (lookDirection != 0) lookDirection = lookDirection;
        }


        ActivateAbilities();
    }

    private void FixedUpdate() {
        if (canMove)
            body.velocity = new Vector2((lookDirection * moveSpeed), body.velocity.y);
    }

    public void SetLookDirection(int direction) {
        Transform transform1 = transform;
        Vector3 localScale = transform1.localScale;
        if (direction == LOOK_DIRECTION_LEFT) {
            if (transform1.localScale.x > 0)
                transform.localScale = new Vector3(transform1.localScale.x * -1, localScale.y,
                    localScale.z);
        } else if (direction == LOOK_DIRECTION_RIGHT)
            if (localScale.x < 0)
                transform.localScale = new Vector3(transform1.localScale.x * -1, localScale.y,
                    localScale.z);
    }

    public void ActivateAbilities() {
        foreach (AbilityController controller in abilities) {
            if (controller.Activate()) return;
        }
    }

    public AbilityController GetActiveAbility() {
        foreach (AbilityController ability in abilities)
            if (ability.IsActive())
                return ability;

        return null;
    }

    public Transform GetCarrierTransform() {
        return transform;
    }

    public int GetLookDirection() {
        if (transform.localScale.x < 0) return -1;

        return 1;
    }

    public void SetAnimationBool(string s, bool b) => animator.SetBool(s, b);
    public void SetAnimationFloat(string s, float f) => animator.SetFloat(s, f);
    public void SetAnimationTrigger(string s) => animator.SetTrigger(s);
    public void SetAnimationInt(string s, int i) => animator.SetInteger(s, i);

    public void SetMobility(bool flag) {
        canTurn = canAttack = canMove = flag;
    }

    private void OnDestroy() {
        input.Dispose();
    }

    public void LoadLevels() {
        upgradeLevels = SaveManager.LoadSaveObject<int>(SaveManager.GetSaveString(filePath))?.GetDictionary();
        foreach (AbilityController controller in abilities) {
            if (upgradeLevels != null && upgradeLevels.TryGetValue(controller.fileString, out _)) {
                controller.SetUpgradeLevel(upgradeLevels[controller.fileString]);
            }
        }
    }

    public void Save() {
        SaveObject<int> saveObject = new SaveObject<int>();
        UpdateUpgradeLevels();
        saveObject.AddAll(upgradeLevels);
        SaveManager.SaveSaveObject(SaveManager.GetSaveString(filePath), saveObject);
        print("saved");
    }

    public void UpdateUpgradeLevels() {
        foreach (AbilityController controller in abilities) {
            upgradeLevels[controller.fileString] = controller.GetUpgradeLevel();
        }
    }

    public void SwitchScenes() {
        foreach (AbilityController controller in abilities) {
            controller.OnSwitchScenes();
        }
    }

    public IEnumerator BackToSaveZone() {
        float time = 0.5f;
        SetMobility(false);
        GameManager.sceneAnimator.FadeOut(time);
        Vector3 freezePos = transform.position;
        float timeElapsed = 0;
        while (timeElapsed < time) {
            timeElapsed += Time.deltaTime;
            transform.position = freezePos;
            yield return null;
        }

        transform.position = lastSafePosition;
        GameManager.sceneAnimator.FadeIn(time);
        SetMobility(true);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(GameManager.Constants.INSTANT_DAMAGE_TAG)) {
            StartCoroutine(BackToSaveZone());
        }

        if (collision.otherCollider == bottomCollider) {
        }

        foreach (AbilityController controller in abilities)
            controller.OnPlayerCollisionEnter2D(collision);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        if (collision.otherCollider == bottomCollider) {
            if (collision.gameObject.layer == GameManager.Constants.STANDABLE_LAYER) {
                isInAir = true;
                animator.SetBool(walkString, false);
            }
        }

        foreach (AbilityController controller in abilities)
            controller.OnPlayerCollisionExit2D(collision);
    }
 
    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.otherCollider == bottomCollider) {
            if (collision.gameObject.CompareTag(GameManager.Constants.STANDABLE_TAG)) {
                lastSafePosition = transform.position;
                if (isInAir) {
                    isInAir = false;
                    isFalling = false;

                    animator.SetTrigger(lowerWingsTrigger);
                }
            }
        }

        foreach (AbilityController controller in abilities)
            controller.OnPlayerCollisionStay2D(collision);
    }
}