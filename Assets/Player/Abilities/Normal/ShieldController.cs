using System;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class ShieldController : AbilityController {
    private static readonly int startFlag = Animator.StringToHash("Start");
    private static readonly int endFlag = Animator.StringToHash("End");


    [Header("Shields")] [SerializeField] private GameObject shield;
    [SerializeField] private Vector2 center;
    [SerializeField] private float radius;


    [Header("Aegis Eclipse")] [SerializeField]
    private float eclipseRegenRate;

    [Header("Elara's Strength")] [SerializeField]
    private float strengthBashDownTime;

    [SerializeField] private float strengthBashDamage;
    [SerializeField] private float strengthBashTime;
    [SerializeField] private float strengthBashDistance;


    private Vector2 bashPosition;
    private Transform shieldTransform;

    private float wantedAngle;
    private float currentAngle;
    private float angleRange = 3 * Mathf.PI / 5;
    private bool isBashing;
    private float timeSinceLastBash;


    // Start is called before the first frame updatei
    private void Start() {
        Setup();
        fileString = "shieldController";
        shieldTransform = shield.transform;
        isBashing = false;
        timeSinceLastBash = 0;
        input.Player.Move.performed += context => {
            Vector2 readInput = context.action.ReadValue<Vector2>();
            wantedAngle = Mathf.Atan2(readInput.y, readInput.x);
            if (wantedAngle < 0 && movement.GetLookDirection() < 0) wantedAngle += Mathf.PI * 2;

            if (movement.GetLookDirection() > 0) wantedAngle = Mathf.Clamp(wantedAngle, -angleRange, angleRange);
            else wantedAngle = Mathf.Clamp(wantedAngle, Mathf.PI - angleRange, Mathf.PI + angleRange);
        };
        input.Player.Shield.started += context => { isPressed = true; };
        input.Player.Shield.canceled += context => {
            isPressed = false;
            Stop();
        };
    }

    // updateQuest is called once per frame
    private void Update() {
        if (!isActive) return;

        currentAngle = Mathf.MoveTowardsAngle(currentAngle, wantedAngle, 360 * Mathf.Deg2Rad * Time.deltaTime);
        Debug.Log(radius);
        shieldTransform.rotation = Quaternion.Euler(0, 0, currentAngle * Mathf.Rad2Deg + (movement.GetLookDirection() < 0 ? 180 : 0));
        shieldTransform.position = transform.position +
                                   new Vector3(center.x * movement.GetLookDirection() + radius * Mathf.Cos(currentAngle), center.y + radius * Mathf.Sin(currentAngle), 0);
    }

    public override bool Activate() {
        if (isActive) return true;
        if (!isPressed) return false;
        if (upgradeLevel <= 0) return false;

        movement.canAttack = false;
        movement.canTurn = false;
        movement.canMove = false;
        body.velocity = new Vector2(0, body.velocity.y);
        currentAngle = wantedAngle;
        isActive = true;
        return true;
    }

    public override bool Stop() {
        if (!isActive) return false;

        movement.canMove = true;
        movement.canTurn = true;
        movement.canAttack = true;
        isActive = false;
        return true;
    }

    public void OnShieldCollision(Collision2D other) {
        if (other.gameObject.layer == GameManager.Constants.ENEMY_LAYER) {
        }
    }
}