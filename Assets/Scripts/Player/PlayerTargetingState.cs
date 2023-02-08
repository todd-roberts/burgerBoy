public class PlayerTargetingState : PlayerBaseState
{
    public bool previouslyEntered = false;

    public PlayerTargetingState(Player player) : base(player)
    {
        Name = "PlayerTargetingState";
    }

    public override void Enter()
    {
        _player.Input.TargetingEvent += this.OnTarget;
        _player.Input.CancelEvent += this.OnCancel;

        if (ShouldAbortEntry())
        {
            OnCancel();
        }
        else
        {
            _player.Animator.Play("TargetingBlendTree");
            _player.Targeter.TargetInitial();
            previouslyEntered = true;
        }

    }

    private bool ShouldAbortEntry()
    {
        bool noTargetsAvailable = !_player.Targeter.HasTargets();

        bool destroyedTargetedEnemy = previouslyEntered && !_player.Targeter.HasValidTarget();

        return noTargetsAvailable || destroyedTargetedEnemy;
    }

    public override void Tick(float deltaTime)
    {
        if (!_player.Targeter.HasValidTarget())
        {
            OnCancel();
            return;
        }

        if (_player.Input.IsAttacking)
        {
            _player.SwitchState(new PlayerAttackingState(_player));
        }
        else
        {
            _player.Move();

            FaceTarget();
        }
    }

    public override void Exit()
    {
        _player.Input.CancelEvent -= this.OnCancel;
        _player.Input.TargetingEvent -= this.OnTarget;
    }

    private void OnTarget() => _player.Targeter.Target();

    private void OnCancel()
    {
        _player.Targeter.ClearTarget();
        _player.SwitchState(new PlayerFreeLookState(_player));
    }
}
