public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy) : base(enemy)
    {
        Name = "EnemyIdleState";
    }

    public override void Tick(float deltaTime)
    {
        _enemy.Character.Idle();

        if (_enemy.Detection.PlayerDetected)
        {
            _enemy.SwitchState(_enemy.GetDetectPlayerState());
        }
    }

    public override void Enter()
    {
        _enemy.Character.IdleRunBlend();
    }

}
