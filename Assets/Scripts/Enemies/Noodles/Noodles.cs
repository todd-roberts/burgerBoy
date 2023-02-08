using UnityEngine;

public class Noodles : Enemy
{
    public override EnemyBaseState GetDetectPlayerState()
    {
        return new LeapAttackState(this);
    }

    public void LeapAttack()
    {

    }
}
