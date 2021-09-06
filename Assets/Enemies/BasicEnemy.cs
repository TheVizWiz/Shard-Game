using System;
using System.Collections;
using System.Collections.Generic;
using BasicEnemyStates;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class BasicEnemy : EnemyStateMachine {


    [SerializeField] private float freezeTime;
    
    // Start is called before the first frame update
    void Start() {  
        base.Start();
        GameObject o = GameManager.playerMain.raycastPosition;
        BasicFindState findEnemy = new BasicFindState(this.enemy, o, () => detectionRadius);
        BasicFindState dontFind = new BasicFindState(this.enemy, o, () => detectionRadius);
        BasicFreezeState waitState = new BasicFreezeState(enemy, freezeTime);
        BasicColorChangeState toRed = new BasicColorChangeState(enemy, Color.red);
        BasicColorChangeState toWhite = new BasicColorChangeState(enemy, Color.white);


        SetStartState(findEnemy);
        AddState("find", findEnemy);
        AddState("wait", waitState);
        AddState("red", toRed);
        AddState("white", toWhite);
        AddState("dontfind", dontFind);

        StateTransition findToRed = new StateTransition(findEnemy, toRed, () => findEnemy.CanSeeObject().Item1);
        StateTransition findToWhite = new StateTransition(dontFind, toWhite, () => !findEnemy.CanSeeObject().Item1);
        StateTransition redToFind = new StateTransition(toRed, dontFind, () => true);
        StateTransition whiteToFind = new StateTransition(toWhite, findEnemy, () => true);

        AddTransition(findToRed);
        AddTransition(findToWhite);
        AddTransition(redToFind);
        AddTransition(whiteToFind);





    }

    // Update is called once per frame
    void Update() {
        base.Update();

    }
}