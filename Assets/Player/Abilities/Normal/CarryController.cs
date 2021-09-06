using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEditor;
using UnityEngine;

public class CarryController : AbilityController {
    [SerializeField] public Vector2 carryPosition;
    [SerializeField] private AnimationCurve pickupSizeChange;
    [SerializeField] private float pickupTime;
    [SerializeField] private float timeBetweenCarries;
    [SerializeField] private float releaseForce;

    [Header("Editor Variables")] public PlayerMovement playerMovement;
    public float editorRadius;


    private Carryable carryable;
    private float z;
    private bool animationDone;
    private Vector3 startScale;


    // Start is called before the first frame update
    void Start() {
        base.Setup();
        movement.input.Player.Strike.started += context => Stop();
        animationDone = false;
    }

    // Update is called once per frame
    void Update() {
        if (!isActive || !animationDone) return;
        carryable.transform.position = transform.position +
                                       new Vector3(carryPosition.x, carryPosition.y,
                                           carryable.transform.position.z);
    }

    public override bool Activate() {
        if (upgradeLevel >= 1 && isActive) {
            return true;
        } else {
            return false;
        }
    }

    public override bool Stop() {
        if (carryable == null) return false;
        if (!isActive) return false;
        if (!animationDone) return false;

        carryable.Release();
        carryable.rigidbody.velocity = Vector2.up * releaseForce;
        isActive = false;

        Vector3 currentScale = carryable.transform.localScale;
        Physics2D.IgnoreLayerCollision(GameManager.Constants.INTERACTABLE_LAYER, GameManager.Constants.PLAYER_LAYER);
        LeanTween.value(this.gameObject, f => {
            carryable.transform.localScale = Vector3.Lerp(currentScale, startScale, f);
        }, 0, 1, timeBetweenCarries).setOnComplete(() => {
            Physics2D.IgnoreLayerCollision(GameManager.Constants.INTERACTABLE_LAYER, GameManager.Constants.PLAYER_LAYER,
                false);
            playerMovement.canAttack = true;
            carryable = null;
        });
        
        
        return true;
    }


    public override void OnPlayerCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(GameManager.Constants.CARRYABLE_TAG)) {
            if (upgradeLevel < 1 || isActive) return;


            isActive = true;
            animationDone = false;
            playerMovement.canAttack = false;
            carryable = collision.gameObject.GetComponent<Carryable>();
            carryable.Pickup();


            Vector3 startPosition = carryable.transform.position;
            z = startPosition.z;
            startScale = carryable.transform.localScale;
            
            LeanTween.value(this.gameObject, f => {
                    carryable.transform.position = Vector3.Lerp(startPosition,
                        new Vector3(carryPosition.x, carryPosition.y, z) + transform.position, f);
                    carryable.transform.localScale = startScale * pickupSizeChange.Evaluate(f);
            }, 0, 1, pickupTime).setEaseOutSine().setOnComplete(() => {
                // carryable.transform.localScale = Vector3.zero;
                animationDone = true;
            });
        }
    }


    public override void OnSwitchScenes() {
        Stop();
    }

    public Transform GetTransform() {
        return transform;
    }

    public override void OnPlayerCollisionStay2D(Collision2D collision) {
    }

    public override void OnPlayerCollisionExit2D(Collision2D collision) {
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(CarryController))]
public class CarryControllerEditor : Editor {
    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    public static void DrawGizmos(CarryController controller, GizmoType type) {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector3) controller.carryPosition + controller.playerMovement.transform.position,
            controller.editorRadius);
        // Gizmos.DrawWireCube(player.transform.position + new Vector3(center.x, center.y, 0), range);
    }
}


#endif