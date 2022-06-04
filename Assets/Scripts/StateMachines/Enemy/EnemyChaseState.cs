using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    public readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.2f;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);

        stateMachine.Animator.SetFloat(SpeedHash, 1f, ANIMATOR_DAMP_TIME, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination = stateMachine.Player.transform.position;
        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
    }
}