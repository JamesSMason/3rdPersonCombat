using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharacterController { get; private set; } = null;
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public NavMeshAgent Agent { get; private set; } = null;
    [field: SerializeField] public WeaponDamage Weapon { get; private set; } = null;
    [field: SerializeField] public Health Health { get; private set; } = null;
    [field: SerializeField] public Target Target { get; private set; } = null;
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; } = null;
    [field: SerializeField] public float MovementSpeed { get; private set; } = 3f;
    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; } = 20;
    [field: SerializeField] public float AttackKnockback { get; private set; } = 5f;

    public Health Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        if (Health != null)
        {
            Health.OnDie += HandleDie;
            Health.OnTakeDamage += HandleTakeDamage;
        }
    }

    private void OnDisable()
    {
        if (Health != null)
        {
            Health.OnDie -= HandleDie;
            Health.OnTakeDamage -= HandleTakeDamage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
}