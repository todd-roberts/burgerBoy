public abstract class PlayerBaseState : State
{
    protected Player _player;

    public PlayerBaseState(Player player)
    {
        _player = player;
    }

    protected void FaceTarget() {
        _player.transform.LookAt(_player.Targeter.GetCurrentTarget().transform);
    }

}
