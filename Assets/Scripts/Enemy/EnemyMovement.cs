using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float chaseDist;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform player;
    private Vector3 distance;
    private bool isAttacking = false;

    void LateUpdate()
    {
        distance = player.transform.position - transform.position;

        if (distance.magnitude <= chaseDist && isAttacking == false)
            Chase();

    }
    public void Chase()
    {
        Vector3 moveDirection = new Vector3(distance.x, 0, distance.z).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveDirection;
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
