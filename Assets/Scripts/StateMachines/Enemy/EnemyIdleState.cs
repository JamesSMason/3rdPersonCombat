using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    public readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.2f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, ANIMATOR_DAMP_TIME, deltaTime);
    }

    public override void Exit()
    {

    }
}