using UnityEngine;

public class TestPlayerState : PlayerBaseState
{
    private float timer = 0f;

    public TestPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        Debug.Log(timer);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new TestPlayerState(stateMachine));
    }
}