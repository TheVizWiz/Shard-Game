using System;
using UnityEngine;

public class FireballController : MonoBehaviour {

    [SerializeField] private float startVelocity;
    [SerializeField] private float movePerSecond;
    [SerializeField] private float targetingPercentage;
    [SerializeField] private float castRadius;
    [SerializeField] private LayerMask enemyLayer;

    private new Collider2D collider;
    private Animator animator;
    private Rigidbody2D body;
    private bool isTargeting;
    private Collider2D[] enemies;

    // Start is called before the first frame update
    private void Start() {
        isTargeting = false;
        enemies = new Collider2D[50];
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        print(enemyLayer);
    }

    public void StartTracking(Vector2 velocity) {
        body.velocity = velocity;
        isTargeting = true;
    }

    // updateQuest is called once per frame
    private void Update() {
        if (Input.GetKeyDown("1")) {
            StartTracking(Vector2.right * startVelocity);
        }
    }

    public void FixedUpdate() {
        if (isTargeting) {
            int numEnemies = Physics2D.OverlapCircleNonAlloc(body.position, castRadius, enemies, enemyLayer);
            if (numEnemies > 0) {
                float distance = Vector2.Distance(enemies[0].transform.position, body.position);
                int index = 0;
                for (int i = 0; i < numEnemies && i < enemies.Length; i++) {
                    float dist = Vector2.Distance(enemies[i].transform.position, body.position);
                    if (dist < distance) {
                        index = i;
                        distance = dist;
                    }
                }

                GameObject closestEnemy = enemies[index].gameObject;
                print(closestEnemy.name);
                Vector3 enemyPos = closestEnemy.transform.position;
                Vector2 direction = new Vector2(enemyPos.x, enemyPos.y) - body.position;
                float force = targetingPercentage * body.mass / direction.sqrMagnitude;
                // body.AddForce(force * direction);
                transform.position = Vector3.MoveTowards(transform.position, enemyPos,
                    movePerSecond * Time.fixedDeltaTime);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer))) {
            isTargeting = false;
            body.velocity = Vector2.zero;
        }
    }
}
