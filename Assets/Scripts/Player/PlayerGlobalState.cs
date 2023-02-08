public class PlayerGlobalState : PlayerBaseState
{
    public PlayerGlobalState(Player player) : base(player)
    {
        Name = "PlayerGlobalState";
    }

    public override void Tick(float deltaTime)
    {
        _player.ApplyGravity();
    }
}
