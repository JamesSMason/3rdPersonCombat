using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 totalMove = motion + stateMachine.ForceReceiver.Movement;
        stateMachine.CharacterController.Move(totalMove * deltaTime);
    }

    protected void FaceTarget(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null) {  return; }

        Vector3 targetDirection = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;

        targetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(targetDirection), deltaTime * stateMachine.RotationDamping);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null) 
        { 
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}