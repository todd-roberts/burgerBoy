public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(Player player): base(player){
        Name = "PlayerTargetingState";
    }

    public override void Enter()
    {
        _player.Input.TargetingEvent += this.OnTarget;
        _player.Input.CancelEvent += this.OnCancel;
        _player.Animator.Play("TargetingBlendTree");
        _player.Targeter.Target();
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();
    }

    public override void Exit()
    {
        _player.Input.CancelEvent -= this.OnCancel;
        _player.Input.TargetingEvent -= this.OnTarget;
        _player.Targeter.ClearTarget();
    }

    private void OnTarget() => _player.Targeter.Target();

    private void OnCancel()
    {
        _player.SwitchState(new PlayerFreeLookState(_player));
    }
}
