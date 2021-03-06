using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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

    protected void FacePlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 targetDirection = stateMachine.Player.transform.position - stateMachine.transform.position;

        targetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    protected void FacePlayer(float deltaTime)
    {
        if (stateMachine.Player == null) { return; }

        Vector3 targetDirection = stateMachine.Player.transform.position - stateMachine.transform.position;

        targetDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(targetDirection), deltaTime * stateMachine.RotationDamping);
    }

    protected bool IsInRange(float range)
    {
        if (stateMachine.Player.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= range * range;
    }
}