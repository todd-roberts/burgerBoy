using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerAttackingState(Player player) : base(player)
    {
        Name = "PlayerAttackingState";
    }

    private int successiveAttacks = 0;

    private const int NTH_POWER_ATTACK = 3;

    public override void Enter()
    {
        successiveAttacks++;

        PerformAttack();
    }

    private void PerformAttack()
    {
        _player.PerformAttack(DetermineAttack());

        successiveAttacks++;
    }

    private AttackName DetermineAttack() => successiveAttacks % NTH_POWER_ATTACK == 0
        ? AttackName.Power
        : AttackName.Basic;

    public override void Tick(float deltaTime)
    {
        //FaceTarget();

        if (AttackHasEnded())
        {
            if (_player.Input.IsAttacking)
            {
                PerformAttack();
            }
            else
            {
                _player.RewindState();
            }
        }
    }


    private bool AttackHasEnded()
    {
        AnimatorStateInfo currentInfo = _player.Animator.GetCurrentAnimatorStateInfo(0);

        return currentInfo.IsTag("Attack") && currentInfo.normalizedTime >= 1.0f;
    }
}
