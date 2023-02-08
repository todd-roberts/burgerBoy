using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobalState : EnemyBaseState
{
    public EnemyGlobalState(Enemy enemy) : base(enemy)
    {
        Name = "EnemyGlobalState";
    }

    public override void Tick(float deltaTime)
    {
        _enemy.ApplyGravity();
    }
}
