using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicEnemyStates {


    public class BasicFreezeState : IEnemyState {
        private float freezeTime;
        private float gravScale;
        private Vector2 freezePosition;
        
        public BasicFreezeState(Enemy enemy, float freezeTime) : base(enemy) {

            this.freezeTime = freezeTime;

        }

        public override void OnEnter() {
            freezePosition = enemy.body.position;
            gravScale = enemy.body.gravityScale;
            enemy.body.gravityScale = 0;
            elapsedTime = 0;

        }

        public override void OnExit() {
            enemy.body.gravityScale = gravScale;
        }

        public override void Update() {
            enemy.body.position = freezePosition;
            elapsedTime += Time.deltaTime; 
        }

        public bool checkDone() {
            return elapsedTime >= freezeTime;
        }
        
    }

    public class BasicFindState : IEnemyState {

        private GameObject findObject;
        private Func<float> detectionRadius;
        public BasicFindState(Enemy enemy, GameObject findObject, Func<float> detectionRadius) : base(enemy) {
            this.findObject = findObject;
            this.detectionRadius = detectionRadius;
        }

        public override void OnEnter() {
        }

        public override void OnExit() {
        }

        public override void Update() {

        }

        public Tuple<bool, Vector2> CanSeeObject() {
            Vector2 position = enemy.body.position;
            Vector2 direction = (Vector2) findObject.transform.position - position;
            RaycastHit2D raycast = Physics2D.Raycast(position, direction, detectionRadius.Invoke());
            return raycast.collider != null && raycast.collider.attachedRigidbody.gameObject.Equals(GameManager.player) ? 
                new Tuple<bool, Vector2>(true, direction) : 
                new Tuple<bool, Vector2>(false, Vector2.zero);
        }
        

    }



    public class BasicColorChangeState : IEnemyState {

        private Color color;
        public BasicColorChangeState(Enemy enemy, Color color) : base(enemy) {
            this.color = color;
        }

        public override void OnEnter() {
            enemy.GetComponent<SpriteRenderer>().color = color;
        }

        public override void OnExit() {
        }

        public override void Update() {
            
        }
    }
}