using Cinemachine;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public CinemachineFreeLook CinemachineFreeLook { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    public Transform MainCameraTransform = null;

    private void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        if (Health != null)
        {
            Health.OnTakeDamage += HandleTakeDamage;
        }
    }

    private void OnDisable()
    {
        if (Health != null)
        {
            Health.OnTakeDamage -= HandleTakeDamage;
        }
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
}