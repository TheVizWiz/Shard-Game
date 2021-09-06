using System.ComponentModel;
using Interfaces;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class StrikeController : AbilityController {
    public static float[] DAMAGE_LEVELS = {10, 15, 20, 25, 30};


    [SerializeField] private Vector2 center;
    [SerializeField] private GameObject trails;
    [SerializeField] private float animationLength;
    public GameObject player1;
    public ParticleSystem leftOverParticles;

    [Header("level 1")] [SerializeField] private float level1ManaRegenRate;
    [SerializeField] private float level1Range;

    [SerializeField] private float level1TimeBetweenActivations;
    // [SerializeField] private float level1DazeDamage;

    [Header("level 2")] [SerializeField] private float level2TimeBetweenActivations;

    [Header("level 3")] [SerializeField] private float level3Range;

    [Header("level 4")] [SerializeField] private float level4DazeDamage;

    [Header("level 5")] [SerializeField] private float level5ManaRegenRate;

    [Header("level 6")] [SerializeField] private float level6Range;

    [Header("level 7")] [SerializeField] private float level7ManaRegenRate;
    [SerializeField] private float level7DazeDamage;


    private float damageLevel;
    private float damage, dazeDamage, range, manaRegenRate, timeBetweenActivations;
    private Collider2D[] objectsHit;
    private ParticleSystem.EmissionModule emissionModule;
    private float particlesTime;


    // Start is called before the first frame update
    private void Start() {
        Setup();
        fileString = "strikeController";
        animString = "Strike";
        objectsHit = new Collider2D[100];
        damage = 12;
        trails.SetActive(false);
        emissionModule = leftOverParticles.emission;
        emissionModule.enabled = false;
        input.Player.Strike.started += context => {
            if (movement.canAttack)
                isPressed = true;
        };
        input.Player.Strike.canceled += context => { isPressed = false; };
    }

    // updateQuest is called once per frame
    private void Update() {
        if (isActive) elapsedTime += Time.deltaTime;
        else timeSinceLastActivation += Time.deltaTime;

        if (isActive && elapsedTime >= animationLength) Stop();
    }

    public override bool Activate() {
        if (isActive) return true;
        if (!movement.canAttack || upgradeLevel <= 0 || !(timeSinceLastActivation >= timeBetweenActivations) ||
            !isPressed) return false;


        //range block
        if (upgradeLevel >= 6) {
            range = level6Range;
        } else if (upgradeLevel >= 3) {
            range = level3Range;
        } else {
            range = level1Range;
        }

        //dazeDamage block
        if (upgradeLevel >= 7) {
            dazeDamage = level7DazeDamage;
        } else if (upgradeLevel >= 4) {
            dazeDamage = level4DazeDamage;
        } else {
            dazeDamage = 0;
        }

        //manaRegenRate block
        if (upgradeLevel >= 7) {
            manaRegenRate = level7ManaRegenRate;
        } else if (upgradeLevel >= 5) {
            manaRegenRate = level5ManaRegenRate;
        } else {
            manaRegenRate = level1ManaRegenRate;
        }

        //activation time block
        if (upgradeLevel >= 2) {
            timeBetweenActivations = level2TimeBetweenActivations;
        } else {
            timeBetweenActivations = level1TimeBetweenActivations;
        }


        animator.SetTrigger("Strike");
        movement.canTurn = false;
        timeSinceLastActivation = 0;
        isActive = true;
        elapsedTime = 0;
        emissionModule.enabled = true;
        trails.SetActive(true);

        int numHit = Physics2D.OverlapCircleNonAlloc(
            body.position + new Vector2(center.x * movement.GetLookDirection(), center.y), range, objectsHit,
            GameManager.Constants.STRIKABLE_LAYERMASK);

        for (int i = 0; i < numHit; i++) {
            try {
                objectsHit[i].gameObject.GetComponent<IStrikable>().Strike(damage, ElementType.Light);
            } catch (MissingComponentException e) {
                Debug.Log(e.Message);
            }
        }

        return false;
    }

    public override bool Stop() {
        if (!isActive) return false;

        isActive = false;
        trails.SetActive(false);
        isPressed = false;
        movement.canTurn = true;
        emissionModule.enabled = false;
        return true;
    }

    public new void Upgrade(int upgradeLevel) {
        base.Upgrade(upgradeLevel);
        animator.SetInteger("StrikeInt", upgradeLevel);
    }

    public new void Upgrade() {
        base.Upgrade();
        animator.SetInteger("StrikeInt", upgradeLevel);
    }


    public Transform GetTransform() {
        return player1.transform;
    }
}