using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected Player _player;

    public PlayerBaseState(Player player)
    {
        _player = player;
    }

    protected void FaceTarget()
    {
        Target target = _player.Targeter.GetCurrentTarget();

        if (target != null)
        {
            Vector3 lookVector = target.transform.position - _player.transform.position;
            lookVector.y = 0f;

            _player.transform.rotation = Quaternion.LookRotation(lookVector);
        }
    }

}
