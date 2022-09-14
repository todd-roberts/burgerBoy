public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(Player player) : base(player){
        Name = "PlayerFreeLookState";
    }
    
    public override void Enter()
    {
        _player.Input.TargetingEvent += this.OnTarget;

        _player.Animator.Play("FreeLookBlendTree");
    }

    public override void Tick(float deltaTime)
    {
        _player.Move();

        if (_player.Input.IsAttacking) {
             _player.SwitchState(new PlayerAttackingState(_player));
        }
    }

    public override void Exit()
    {
        _player.Input.TargetingEvent -= this.OnTarget;
    }

    private void OnTarget()
    {
        if (_player.Targeter.HasTargets()) {
            _player.SwitchState(new PlayerTargetingState(_player));
        }
    }
  
}
