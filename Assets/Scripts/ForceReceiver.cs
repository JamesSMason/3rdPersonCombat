using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController Controller = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private float drag = 0.3f;
    [SerializeField] private float impactMagnitudeCheck = 0.2f;

    private Vector3 impact;
    private Vector3 dampingVelocity;
    private float verticalVelocity;


    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    void Update()
    {
        if (verticalVelocity < 0f && Controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (impact.sqrMagnitude <= impactMagnitudeCheck * impactMagnitudeCheck)
        {
            if (agent != null)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}