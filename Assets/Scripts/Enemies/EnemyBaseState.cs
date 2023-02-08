using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected Enemy _enemy;

    public EnemyBaseState(Enemy enemy)
    {
        _enemy = enemy;
    }

}
