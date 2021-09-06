using UnityEngine;

public abstract class AbilityController : MonoBehaviour {

    protected GameObject player;
    protected Animator animator;
    protected Rigidbody2D body;
    protected PlayerMovement movement;
    protected PlayerMain main;
    protected new Transform transform;

    protected float elapsedTime;
    protected float elapsedChargeTime;
    protected float chargeTime;
    protected float timeSinceLastActivation;
    protected bool isActive;
    protected bool isCharged;
    protected bool isCharging;
    protected string animString;
    protected bool isPressed;
    
    [HideInInspector] public string fileString;
    [HideInInspector] public PlayerInput input;


    [SerializeField] protected int upgradeLevel;



    public abstract bool Activate();

    public abstract bool Stop();

    public bool IsActive() {
        return isActive;
        
    }

    public int GetUpgradeLevel() {
        return upgradeLevel;
    }

    public void SetUpgradeLevel(int uglevel) {
        upgradeLevel = uglevel;
    }

    public void Setup() {
        player = GameManager.player;
        isActive = false;
        elapsedTime = 0;
        timeSinceLastActivation = 0;
        animator = player.GetComponent<Animator>();
        body = player.GetComponent<Rigidbody2D>();
        movement = GameManager.playerMovement;
        main = GameManager.playerMain;
        transform = player.transform;
        input = movement.input;
    }

    public void Upgrade(int upgradeLevel) {
        this.upgradeLevel = upgradeLevel;
    }

    public int Upgrade() => upgradeLevel++;


    public virtual void OnSwitchScenes() {
        
    }

    public virtual void OnPlayerCollisionEnter2D(Collision2D collision) {
    }

    public virtual void OnPlayerCollisionStay2D(Collision2D collision) {
    }

    public virtual void OnPlayerCollisionExit2D(Collision2D collision) {
    }

}