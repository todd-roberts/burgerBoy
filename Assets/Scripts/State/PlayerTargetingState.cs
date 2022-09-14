using UnityEngine;

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
        _player.Targeter.TargetInitial();
    }

    public override void Tick(float deltaTime)
    {
        if (!_player.Targeter.HasValidTarget()) {
            OnCancel();
            return;
        }

        if (_player.Input.IsAttacking) {
            _player.SwitchState(new PlayerAttackingState(_player));
            return;
        }

        _player.Move();

        FaceTarget();
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
