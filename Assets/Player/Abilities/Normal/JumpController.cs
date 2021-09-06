using UnityEngine;
using UnityEngine.Serialization;

public class JumpController : AbilityController {
    private bool hasJumped;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float maxTime;


    // Start is called before the first frame update

    private void Start() {
        Setup();
        fileString = "jumpController";
        animString = "Jump";
        input.Player.Jump.started += context => {
            isPressed = true;
        };
        input.Player.Jump.canceled += context => {
            isPressed = false;
        };
    }


    private void FixedUpdate() {
        if (isActive) {
            if (!movement.canMove) {
                Stop();
            }

            if (isPressed && elapsedTime < maxTime) {
                elapsedTime += Time.deltaTime;
                // body.AddForce(Physics2D.gravity * (-1 * body.gravityScale));//
                body.velocity = Vector2.up * jumpVelocity;
            } else if (!isPressed) {
                Stop();
            }

            if (elapsedTime >= maxTime) {
                Stop();
                body.velocity = Vector2.up * jumpVelocity;
            }
        }
    }

    public override bool Activate() {
        if (movement.canInteract) return false;
        if (!hasJumped && !movement.isInAir && isPressed) {
            hasJumped = true;
            isActive = true;
            elapsedTime = 0;
            animator.SetBool(PlayerMovement.jumpString, true);
            return true;
        }

        return false;
    }

    public override bool Stop() {
        if (!isActive) return false;

        isActive = false;
        hasJumped = false;
        isPressed = false;
        body.velocity = new Vector2(body.velocity.x, 0);
        animator.SetBool(PlayerMovement.jumpString, false);
        return true;
    }

    public override void OnPlayerCollisionEnter2D(Collision2D collision) {
        if (isActive && collision.gameObject.layer == GameManager.Constants.STANDABLE_LAYER) {
            hasJumped = false;
            Stop();
            elapsedTime = 0;
        }
    }
}