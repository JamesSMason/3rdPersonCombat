using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController Controller = null;

    private float verticalVelocity;

    public Vector3 Movement => Vector3.up * verticalVelocity;

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
    }
}