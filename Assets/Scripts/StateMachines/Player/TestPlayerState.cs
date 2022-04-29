using UnityEngine;

public class TestPlayerState : PlayerBaseState
{
    private float timer = 5f;

    public TestPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        Debug.Log(timer);

        if (timer <= 0)
        {
            stateMachine.SwitchState(new TestPlayerState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}