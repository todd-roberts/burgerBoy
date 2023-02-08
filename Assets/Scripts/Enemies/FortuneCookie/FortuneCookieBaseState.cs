using UnityEngine;

public abstract class FortuneCookieBaseState : EnemyBaseState
{
    protected FortuneCookie _fortuneCookie;

    public FortuneCookieBaseState(FortuneCookie fortuneCookie) : base(fortuneCookie)
    {
        _fortuneCookie = fortuneCookie;
    }

}