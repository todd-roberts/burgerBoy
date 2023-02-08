using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(Player player) : base(player)
    {
        Name = "PlayerFreeLookState";
    }

    public override void Enter()
    {
        // _player.Input.TargetingEvent += this.OnTarget;
        _player.Input.JumpEvent += this.OnJump;
        _player.Animator.CrossFadeInFixedTime("FreeLookBlendTree", .1f);
    }

    public override void Tick(float deltaTime)
    {
        _player.Move();

        if (_player.Input.IsAttacking)
        {
            _player.SwitchState(new PlayerAttackingState(_player));
        }
    }

    public override void Exit()
    {
        // _player.Input.TargetingEvent -= this.OnTarget;
    }

    private void OnTarget()
    {
        if (_player.Targeter.HasTargets())
        {
            _player.SwitchState(new PlayerTargetingState(_player));
        }
    }

    private void OnJump()
    {
        Debug.Log("Jumping...");

    }
}
