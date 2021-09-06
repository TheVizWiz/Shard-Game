using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateMachine : StateMachine {
    // Start is called before the first frame update
    
    [SerializeField] protected float detectionRadius;
    protected Enemy enemy;
    

    public void Start() {
        base.Start();
        this.enemy = GetComponent<Enemy>();
        this.detectionRadius = detectionRadius;
    }
    
}
