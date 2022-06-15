using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CROSS_FADE_DURATION = 0.2f;

    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
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
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }
}