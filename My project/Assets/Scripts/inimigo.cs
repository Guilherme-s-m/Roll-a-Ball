using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyFollower : MonoBehaviour
{
    public Transform target; // Assign the player's transform
    public float speed = 4f; // Movement speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 velocity = direction * speed;

        // Move the follower towards the target position
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
