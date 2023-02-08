using UnityEngine;

public class CastState : FortuneCookieBaseState
{
    public CastState(FortuneCookie fortuneCookie) : base(fortuneCookie)
    {
        Name = "CastState";
    }

    private bool _hasCasted = false;

    public override void Enter()
    {
        FacePlayer();

        _fortuneCookie.Character.Cast();
    }

    private void FacePlayer()
    {
        Vector3 direction = -1 * Vector3.Normalize(_fortuneCookie.transform.position - _fortuneCookie.GetPlayerPosition());

        direction.y = 0f;

        _fortuneCookie.transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Tick(float deltaTime)
    {
        if (_fortuneCookie.Character.CastComplete())
        {
            _fortuneCookie.SwitchState(new EnemyIdleState(_fortuneCookie));
        }
        else if (_hasCasted)
        {
            return;
        }
        else if (_fortuneCookie.Character.ShouldCastSpell())
        {
            _fortuneCookie.Cast();

            _hasCasted = true;
        }

    }
}