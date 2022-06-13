using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TRANSITION_DURATION = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TRANSITION_DURATION);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}