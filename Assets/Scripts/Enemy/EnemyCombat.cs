using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] protected float attackDist;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Transform player;
    [SerializeField] protected float onAttackedCooldown;
    protected Animator animator;
    protected float timer;
    protected float onAttackedTimer;
    public UnityEvent OnTargetAttack;
    public UnityEvent OnLeaveAttackRange;
    private Vector3 distance;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        onAttackedTimer -= Time.deltaTime;
        distance = player.transform.position - transform.position;
        PrepareAttack();
    }

    public void OnAttacked() => onAttackedTimer = onAttackedCooldown;

    public void PrepareAttack()
    {
        if (timer <= 0 && onAttackedTimer <= 0)
        {
            if (distance.magnitude <= attackDist)
            {
                OnTargetAttack?.Invoke();

                timer = attackDelay;
                OnAttack();
            }

            if (distance.magnitude >= attackDist)
            {
                OnLeaveAttackRange?.Invoke();
            }
        }
    }
}
