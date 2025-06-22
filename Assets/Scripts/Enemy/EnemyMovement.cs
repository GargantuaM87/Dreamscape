using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float chaseDist;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;
    private Rigidbody rb;
    private Vector3 distance;
    private bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        distance = player.transform.position - transform.position;

        if (distance.magnitude <= chaseDist && isAttacking == false)
        {
            Vector3 moveDirection = new Vector3(distance.x, 0, distance.z).normalized;
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
        }
    }


    public void StopChase()
    {
        isAttacking = true;
    }

    public void ContinueChase()
    {
        isAttacking = false;
    }
}
