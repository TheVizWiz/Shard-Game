using System;
using System.Collections;
using UnityEngine;

public class ShiftedDiveController : AbilityController {
    [Header("Level 1")] //UG1 variables
    [SerializeField]
    private float level1ManaCost;

    [SerializeField] private float level1ChargeTime;
    [SerializeField] private float level1Speed;
    [SerializeField] private float level1DirectDamage;

    [Header("Level 2")] //UG2 variables
    [SerializeField]
    private float level2DirectDamage;

    [Header("Level 3")] //UG3 variables
    [SerializeField]
    private float level3AreaDamage;

    [SerializeField] private Vector2 level3AreaBox;

    [Header("Level 4")] //UG5 variables -> exotic level
    [SerializeField]
    private float level4ManaCost;

    [SerializeField] private float level4Speed;
    //no charge time

    [Header("Level 6")] [SerializeField] private float level6DirectDamage;
    [SerializeField] private float level6AreaDamage;

    [Header("Level 7")] [SerializeField] private float level7DazeAmount;

    [Header("Level 8")] [SerializeField] private float level8ManaPerSecond;
    [SerializeField] private float level8DazeAmount;
    [SerializeField] private float level8ManaCost;
    [SerializeField] private float level8StoppingForce;
    [SerializeField] private float level8StoppingTime;
    [SerializeField] private Vector2 level8AreaBox;


    private float manaCost, directDamage, areaDamage, speed, manaReturnSpeed, dazeAmount;
    private bool isStopping;
    private float elapsedStopTime;
    private Vector2 areaBox;
    private Vector2 tempPosition;
    private Collider2D[] enemiesHit;

    private bool jumpPressed;


    // Start is called before the first frame update
    private void Start() {
        Setup();
        fileString = "diveController";
        isCharged = false;
        isStopping = false;
        enemiesHit = new Collider2D[100];
        input.Player.Dive.started += context => { isPressed = true; };
        input.Player.Dive.canceled += context => {
            isPressed = false; 
            
        };
        input.Player.Jump.started += context => {
            if (isActive) jumpPressed = true;
        };
        input.Player.Jump.canceled += context => { jumpPressed = false; };
    }

    // updateQuest is called once per frame
    private void FixedUpdate() {
        if (isActive) {

            if (isCharging) {
                elapsedChargeTime += Time.deltaTime;
                body.MovePosition(tempPosition);
                if (elapsedChargeTime >= chargeTime) {
                }

                if (!isPressed) {
                    if (elapsedChargeTime >= chargeTime) {
                        isCharging = false;
                        isCharged = true;
                        elapsedChargeTime = 0;
                    } else {
                        Stop();
                    }
                }
            }

            if (isCharged) {
                body.MovePosition(body.position + Vector2.down * speed);
                if (upgradeLevel >= 8) {
                    main.AddMana(manaReturnSpeed * Time.deltaTime);
                }
            }

            if (!isStopping && upgradeLevel >= 8 && jumpPressed) {
                isStopping = true;
                tempPosition = body.position;
                // Stop();
                // body.velocity = Vector2.up * level8VelocityWhenStopping;
            }

            if (isStopping) {
                elapsedStopTime += Time.deltaTime;
                body.MovePosition(tempPosition);
                if (elapsedStopTime >= level8StoppingTime) {
                    isStopping = false;
                    elapsedStopTime = 0;
                    Stop();
                }
            }

            elapsedTime += Time.deltaTime;
        }
    }

    public override bool Activate() {
        if (isActive) return true;
        if (upgradeLevel <= 0 || !movement.isInAir || !movement.canMove || !isPressed) return false;
        if (!movement.isShifted) return false;

        //charge time block
        if (upgradeLevel >= 4) {
            chargeTime = 0;
            isCharging = false;
            isCharged = true;
        } else {
            chargeTime = level1ChargeTime;
            isCharged = false;
            isCharging = true;
        }


        //mana cost block
        if (upgradeLevel >= 8)
            manaCost = level8ManaCost;
        else if (upgradeLevel >= 4)
            manaCost = level4ManaCost;
        else
            manaCost = level1ManaCost;


        //mana return block
        if (upgradeLevel >= 8)
            manaReturnSpeed = level8ManaPerSecond;
        else
            manaReturnSpeed = 0;

        //drop speed block
        if (upgradeLevel >= 4)
            speed = level4Speed;
        else
            speed = level1Speed;

        //direct damage block
        if (upgradeLevel >= 6)
            directDamage = level6DirectDamage;
        else if (upgradeLevel >= 2)
            directDamage = level2DirectDamage;
        else
            directDamage = level1DirectDamage;

        //area damage block
        if (upgradeLevel >= 6)
            areaDamage = level6AreaDamage;
        else if (upgradeLevel >= 3)
            areaDamage = level3AreaDamage;
        else
            areaDamage = 0;

        //area areaBox block
        if (upgradeLevel >= 8)
            areaBox = level8AreaBox;
        else if (upgradeLevel >= 3)
            areaBox = level3AreaBox;
        else
            areaBox = Vector2.zero;

        //daze amount block
        if (upgradeLevel >= 8)
            dazeAmount = level8DazeAmount;
        else
            dazeAmount = level7DazeAmount;


        if (!main.UseMana(manaCost)) return false;
        isActive = true;
        movement.canTurn = false;
        movement.canMove = false;
        movement.canAttack = false;
        tempPosition = body.position;
        elapsedChargeTime = 0;
        return true;

    }

    public override bool Stop() {
        if (!isActive) return false;
        isActive = false;
        body.velocity = Vector2.zero;
        movement.canTurn = true;
        movement.canMove = true;
        movement.canAttack = true;
        elapsedChargeTime = 0;
        elapsedTime = 0;
        return true;
    }

    public override void OnPlayerCollisionEnter2D(Collision2D collision) {
        if (!isActive) return;
        
        if (collision.gameObject.layer == GameManager.Constants.STANDABLE_LAYER) {
            if (upgradeLevel >= 3) {
                int numHit = Physics2D.OverlapBoxNonAlloc(body.position, areaBox, 0, enemiesHit,
                    GameManager.Constants.DIVABLE_LAYERMASK);

                for (int i = 0; i < numHit; i++) {
                    try {
                        enemiesHit[i].GetComponent<Enemy>().Strike(areaDamage, ElementType.Earth);
                    } catch (MissingComponentException e) {
                        print("enemy has no Enemy Script");
                    }
                }
            }

            Stop();
        } else if (collision.gameObject.layer == GameManager.Constants.ENEMY_LAYER) {
            if (!movement.isInAir) return;
            if (upgradeLevel >= 2) {
                collision.gameObject.GetComponent<Enemy>().Strike(directDamage, ElementType.Earth);
            } else {
                main.Damage(collision.gameObject.GetComponent<Enemy>().playerDamageMultiplier);
            }
        }
    }
}