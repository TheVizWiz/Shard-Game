using System.Collections;
using UnityEngine;

public class DashController : AbilityController {
    [SerializeField] private ParticleSystem[] particles;

    // private ParticleSystem.EmissionModule emissionModule;
    [SerializeField] private TrailRenderer[] trails;
    [SerializeField] private float dashTime;

    [Header("Level 1")] [SerializeField] private float distance;
    [SerializeField] private float level1TimeBetweenActivations;

    [Header("Level 2")] [SerializeField] private float level2TimeBetweenActivations;

    [Header("Level 3")] [SerializeField] private float level3timeBetweenExplosions;
    [SerializeField] private float level3EnemyDamage;
    [SerializeField] private float level3ChargeTime;
    [SerializeField] private float level3ExplosionRadius;

    [Header("Level 4")] [SerializeField] private float level4EnemyDamage;
    [SerializeField] private float level4ExplosionRadius;

    [Header("Level 5")] [SerializeField] private float dazeTimer;
    [SerializeField] private float level5TimeBetweenActivations;
    [SerializeField] private float level5TimeBetweenExplosions;

    private Vector2 dashPosition;
    private Collider2D[] explodedHitEnemyColliders;
    private float distanceDashed;
    private int lookDirection;
    private float explosionDistance;
    private float timeSinceLastExplosion;
    private bool exploded;
    private bool strikePressed;
    private static readonly int dashString = Animator.StringToHash("Dash");


    // Start is called before the first frame update
    private void Start() {
        Setup();
        fileString = "dashController";
        // emissionModule = particles.emission;
        timeSinceLastActivation = Mathf.Infinity;
        timeSinceLastExplosion = Mathf.Infinity;
        distanceDashed = 0;
        exploded = false;
        isCharging = false;
        isCharged = false;
        explodedHitEnemyColliders = new Collider2D[] { };
        SetParticleEmissions(false);
        SetTrailEmissions(false);
        input.Player.Move.performed += context => {
            if (context.ReadValue<Vector2>().x < 0) lookDirection = -1;
            else lookDirection = 1;
        };
        input.Player.Dash.started += context => {
            if (!isActive) isPressed = true;
        };
        input.Player.Dash.canceled += context => { isPressed = false; };
        input.Player.Strike.started += context => {
            if (isActive) strikePressed = true;
        };
        input.Player.Strike.canceled += context => { strikePressed = false; };
        // trail.emitting = false;
        // emissionModule.enabled = false;u
    }

    // updateQuest is called once per frame
    private void Update() {
        if (!isActive) {
            timeSinceLastActivation += Time.deltaTime;
            timeSinceLastExplosion += Time.deltaTime;
            return;
        }

        if (upgradeLevel >= 3) {
            timeSinceLastExplosion += Time.deltaTime;
            Physics2D.IgnoreLayerCollision(GameManager.Constants.PLAYER_LAYER, GameManager.Constants.ENEMY_LAYER);
        }

        // print("ischarging = " + isCharging + ", ischarged = " + isCharged + ", isActive = " + isActive);
        //checking for explosions for upper tiers
        if (!exploded) {
            if (movement.canAttack && isPressed && strikePressed) {
                if (((upgradeLevel == 4 || upgradeLevel == 3) &&
                     timeSinceLastExplosion >= level3timeBetweenExplosions) ||
                    (upgradeLevel == 5 && timeSinceLastExplosion >= level5TimeBetweenExplosions)) {
                    exploded = true;
                    timeSinceLastExplosion = 0;
                    Explode(transform.position);
                }
            }
        }

        //moving the player per frame while active
        float moveAmount = distance / dashTime * Time.deltaTime;
        distanceDashed += lookDirection * moveAmount;
        dashPosition += Vector2.right * (lookDirection * moveAmount);
        body.MovePosition(dashPosition);
        elapsedTime += Time.deltaTime;
        if (elapsedTime > dashTime) {
            Stop();
            Physics2D.IgnoreLayerCollision(GameManager.Constants.PLAYER_LAYER, GameManager.Constants.ENEMY_LAYER,
                false);
        }
    }

    public override bool Activate() {
        if (isActive) return true;
        if (isActive || !movement.canMove || !isPressed) return false;
        bool willActivate = false;


        switch (upgradeLevel) {
            case 0:
                return false;
            case 1 when timeSinceLastActivation >= level1TimeBetweenActivations:
                willActivate = true;
                isCharging = false;
                isCharged = false;
                break;
            default: 
                if (upgradeLevel < 5 && upgradeLevel > 1 &&
                    timeSinceLastActivation >= level2TimeBetweenActivations) {
                    willActivate = true;
                    isCharging = upgradeLevel > 2;
                } else if (upgradeLevel == 5 && timeSinceLastActivation >= level5TimeBetweenActivations) {
                    willActivate = true;
                    isCharging = true;
                }

                break;
        }

        if (!willActivate) return false;
        
        isActive = true;
        elapsedTime = 0;
        SetTrailEmissions(true);
        SetParticleEmissions(true);
        animator.SetTrigger(dashString);
        movement.canMove = false;
        movement.canTurn = false;
        dashPosition = body.position;
        body.velocity = Vector2.zero;
        timeSinceLastActivation = 0;
        return true;

    }

    public override bool Stop() {
        if (!isActive) return false;
        elapsedTime = 0;
        distanceDashed = 0;
        isCharged = false;
        isCharging = false;
        exploded = false;
        isActive = false;
        SetTrailEmissions(false);
        SetParticleEmissions(false);
        movement.canTurn = true;
        movement.canMove = true;
        return true;
    }

    public void Explode(Vector3 position) {
        float waitTime = 1.0f;
        float damage = (upgradeLevel >= 4) ? level4EnemyDamage : level3EnemyDamage;
        float explosionRadius = (upgradeLevel >= 4) ? level4ExplosionRadius : level3ExplosionRadius;
        bool chainDamage = upgradeLevel >= 5;

        StartCoroutine(
            Explosion.CreateExplosion(position, explosionRadius, damage, ElementType.Dark, chainDamage,
                GameManager.Constants.ENEMY_LAYERMASK, waitTime));
    }

    private void SetParticleEmissions(bool flag) {
        foreach (ParticleSystem system in particles) {
            if (flag) {
                system.Play();
            } else {
                system.Stop();
            }
        }
    }

    private void SetTrailEmissions(bool flag) {
        foreach (TrailRenderer trailRenderer in trails) {
            trailRenderer.emitting = flag;
        }
    }
}