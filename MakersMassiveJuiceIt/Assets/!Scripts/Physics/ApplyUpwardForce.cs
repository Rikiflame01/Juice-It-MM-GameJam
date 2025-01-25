using UnityEngine;

public class ApplyUpwardForce : MonoBehaviour
{
    Rigidbody body;

    [SerializeField] private int forceAmount = 5;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        body.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);

    }
}
