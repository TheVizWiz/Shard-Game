using UnityEngine;
using UnityEngine.Serialization;

public class DoubleJumpController : AbilityController {
    private bool hasJumped;

    [SerializeField] private float jumpVelocity;

    [SerializeField] private float maxTime;
    // Start is called before the first frame update

    private void Start() {
        Setup();
        fileString = "doubleJumpController";
        animString = "DoubleJump";
        input.Player.Jump.started += context => {
            if (movement.isInAir) 
                isPressed = true;
        };
        input.Player.Jump.canceled += context => { isPressed = false; };
    }

    // updateQuest is called once per frame
    private void FixedUpdate() {
        // Vector3.MoveTowards(transform.position, transform.position, 1);

        if (isActive) {
            if (!movement.canMove) {
                Stop();
            }

            if (isPressed && elapsedTime < maxTime) {
                elapsedTime += Time.deltaTime;
                // body.AddForce(Physics2D.gravity * (-1 * body.gravityScale));
                body.velocity = Vector2.up * jumpVelocity;
            } else if (!isPressed) {
                Stop();
            }

            if (elapsedTime >= maxTime) {
                isActive = false;
                Stop();
                elapsedTime = 0;
            }
        }
    }

    public override bool Activate() {
        if (!hasJumped && movement.isInAir && upgradeLevel > 0 && isPressed) {
            hasJumped = true;
            isActive = true;
            elapsedTime = 0;
            animator.SetBool(PlayerMovement.doubleJumpString, true);
            // body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            return true;
        }

        return false;
    }

    public override bool Stop() {
        if (!isActive) return false;
        isActive = false;
        isPressed = false;
        body.velocity = new Vector2(body.velocity.x, 0);
        animator.SetBool(PlayerMovement.doubleJumpString, false);
        return true;
    }

    public override void OnPlayerCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(GameManager.Constants.STANDABLE_TAG)) {
            hasJumped = false;
            Stop();
        }
    }
}