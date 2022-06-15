using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CROSS_FADE_DURATION = 0.2f;

    private float duration = 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}