public class PlayerFreeLookState : PlayerBaseState
{

    public PlayerFreeLookState(Player player) : base(player){}

    private void OnJump()
    {
        
    }

    public override void Enter()
    {
        _player.Input.JumpEvent += this.OnJump;
    }

    public override void Tick(float deltaTime)
    {
        _player.Move(deltaTime);
    }

    public override void Exit()
    {
        _player.Input.JumpEvent -= this.OnJump;
       
    }
}
