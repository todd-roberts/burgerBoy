using UnityEngine;

public class LeapAttackState : EnemyBaseState
{
    public LeapAttackState(Enemy enemy) : base(enemy)
    {
        Name = "LeapAttackState";
    }

    public override void Enter()
    {
        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 direction = -1 * Vector3.Normalize(_enemy.transform.position - _enemy.GetPlayerPosition());

        direction.y = 0f;

        _enemy.transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Tick(float deltaTime)
    {
        // TODO
    }
}
