using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private CharacterController characterController = null;

    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;

    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>();
        allRigidbodies = GetComponentsInChildren<Rigidbody>();

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allColliders)
        {
            if (collider.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rigidbody in allRigidbodies)
        {
            rigidbody.isKinematic = !isRagdoll;
            rigidbody.useGravity = isRagdoll;
        }

        characterController.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}