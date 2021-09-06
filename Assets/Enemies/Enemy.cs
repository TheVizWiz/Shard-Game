using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IStrikable, ISlashable, IExplodable {

    public float health;
    public int playerDamageMultiplier;
    public float weakenMultiplier;
    private float weakenTimeLeft;

    [HideInInspector] public int lookDirection;


    public Rigidbody2D body;
    public new Transform transform;
    public Animator animator;


    public void Awake() {
        body = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    public void Update() {
        weakenTimeLeft -= Time.deltaTime;
        if (weakenTimeLeft <= 0) weakenMultiplier = 0;
    }

    public bool Explode(float damage, ElementType element) {
        health -= damage * (1 - weakenMultiplier);
        if (CheckDeath()) {
            Die();
            print(name + " died from explosion");
            return true;
        }

        return false;
    }

    public bool Strike(float damage, ElementType element) {
        health -= damage * (1 - weakenMultiplier);;
        if (CheckDeath()) {
            Die();
            print(name + " died from Strike");
            return true;
        } 

        return false;

    }

    public void Daze(float amount) {
        
    }
    

    public bool Slash(float damage, float throwForce, float weakenAmount, float weakenTime, ElementType element) {
        
        health -= damage * (1 - weakenMultiplier);
        if (weakenAmount > weakenMultiplier) {
            weakenMultiplier = weakenAmount;
            weakenTimeLeft = weakenTime;
        }
        body.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
        if (CheckDeath()) {
            Die();
            return true;
        }
        return false;
    }

    public void Die() {
        Destroy(gameObject);
    }

    public bool CheckDeath() {
        return health <= 0;
    }
    
    
    
    
}