public abstract class PlayerBaseState : State
{
    protected Player _player;

    public PlayerBaseState(Player player)
    {
        _player = player;
    }
}
