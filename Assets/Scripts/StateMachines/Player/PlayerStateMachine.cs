using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    public Transform MainCameraTransform = null;

    void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void Start()
    {
        SwitchState(new TestPlayerState(this));
    }
}