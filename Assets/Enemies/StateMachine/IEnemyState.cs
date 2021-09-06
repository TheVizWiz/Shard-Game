using System;
using UnityEngine;

public abstract class IEnemyState : IState {

    protected Enemy enemy;
    protected float elapsedTime;

    public IEnemyState (Enemy enemy) {
        this.enemy = enemy;
    }
}