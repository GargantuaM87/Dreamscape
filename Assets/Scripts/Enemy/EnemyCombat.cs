using UnityEngine;
using UnityEngine.Events;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] protected float attackDist;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Transform player;
    protected Animator animator;
    protected float timer;
    public UnityEvent OnTargetAttack;
    public UnityEvent OnLeaveAttackRange;

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
        PrepareAttack();
    }

    public void PrepareAttack()
    {
        if (timer <= 0)
        {
            Vector3 distance = player.transform.position - transform.position;
            if (distance.magnitude <= attackDist)
            {
                OnTargetAttack?.Invoke();

                timer = attackDelay;
                OnAttack();
            }
            else
                animator.SetTrigger("Rest"); 

            if (distance.magnitude >= attackDist)
            {
                OnLeaveAttackRange?.Invoke();
                animator.SetTrigger("Rest");
            }
                
        }
    }
}
