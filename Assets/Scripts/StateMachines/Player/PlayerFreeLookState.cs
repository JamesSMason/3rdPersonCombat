using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    public readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.2f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, ANIMATOR_DAMP_TIME, deltaTime);
            return; 
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, ANIMATOR_DAMP_TIME, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}