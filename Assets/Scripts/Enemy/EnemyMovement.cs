using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float chaseDist;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;
    private Rigidbody rb;
    private Vector3 distance;
    private bool isAttacking = false;
    private EnemyCombat enemyCombat;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyCombat = GetComponent<EnemyCombat>();
    }


    void FixedUpdate()
    {
        distance = player.transform.position - transform.position;
        float length = distance.magnitude;

        if (length < chaseDist && length > enemyCombat.AttackDist && isAttacking == false)
        {
            Vector3 moveDirection = new Vector3(distance.x, 0, distance.z).normalized;
            // rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
            rb.linearVelocity = moveSpeed * Time.fixedDeltaTime * moveDirection;
        }
        else
            rb.linearVelocity = Vector3.zero;
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
