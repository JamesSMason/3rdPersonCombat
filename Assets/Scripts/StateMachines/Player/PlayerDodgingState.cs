using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardSpeedHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightSpeedHash = Animator.StringToHash("DodgeRight");

    private const float CROSS_FADE_DURATION = 0.2f;

    private Vector3 dodgingDirectionInput;
    private float remainintDodgeTime;

    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirection) : base(stateMachine)
    {
        dodgingDirectionInput = dodgingDirection;
    }

    public override void Enter()
    {
        remainintDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgeForwardSpeedHash, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightSpeedHash, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CROSS_FADE_DURATION);

        stateMachine.Health.SetIsInvulnerable(true);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move(movement, deltaTime);
        FaceTarget(deltaTime);

        remainintDodgeTime -= deltaTime;

        if (remainintDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetIsInvulnerable(false);
    }
}