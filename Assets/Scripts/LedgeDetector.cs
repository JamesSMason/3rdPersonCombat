using System;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnLedgeDetect;

    private void OnTriggerEnter(Collider other)
    {
        OnLedgeDetect?.Invoke(other.ClosestPoint(transform.position), other.transform.forward);
    }
}