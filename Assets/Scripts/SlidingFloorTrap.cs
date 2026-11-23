
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlidingFloorTrap : MonoBehaviour
{
    public float slideDistance = 2f;
    public float slideSpeed = 1.5f;

    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        startPosition = rb.position;
    }

    void FixedUpdate()
    {
        float movement = Mathf.PingPong(
            Time.fixedTime * slideSpeed,
            slideDistance
        );

        Vector3 targetPosition =
            startPosition + transform.right * movement;

        rb.MovePosition(targetPosition);
    }
}
