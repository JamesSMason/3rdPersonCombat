using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 totalMove = motion + stateMachine.ForceReceiver.Movement;
        stateMachine.CharacterController.Move(totalMove * deltaTime);
    }
}