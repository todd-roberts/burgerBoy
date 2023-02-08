using UnityEngine;

public class EnemyDamagedState : EnemyBaseState
{
    private bool _enemyStunned;

    private float _timeStunned;
    public EnemyDamagedState(Enemy enemy) : base(enemy)
    {
        Name = "EnemyDamagedState";
    }

    public override void Tick(float deltaTime)
    {
        if (_enemy.Character.DamageComplete())
        {
            Debug.Log("this never happens");
            _timeStunned += deltaTime;

            if (_timeStunned >= _enemy.GetStunDuration())
            {
                _enemy.SwitchState(new EnemyIdleState(_enemy));
            }
        }
        Debug.Log("Still in here lol");
    }

    public override void Enter()
    {
        _enemy.Character.Damage();
    }

}
