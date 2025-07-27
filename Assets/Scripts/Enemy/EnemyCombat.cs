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
    protected private Vector3 distance;
    public UnityEvent OnTargetAttack;
    public UnityEvent OnLeaveAttackRange;

    #region monobehaviour
    void Start()
    {
        animator = GetComponent<Animator>();
        if (player == null)
            player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        onAttackedTimer -= Time.deltaTime;
        distance = player.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        PrepareAttack();
    }
    #endregion
    public void OnAttacked() => onAttackedTimer = onAttackedCooldown;

    public virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PrepareAttack()
    {
        if (timer <= 0 && onAttackedTimer <= 0)
        {
            if (distance.magnitude < attackDist)
            {
                OnTargetAttack?.Invoke();

                timer = attackDelay;
                OnAttack();
            }

            if (distance.magnitude > attackDist)
            {
                OnLeaveAttackRange?.Invoke();
            }
        }
    }
    //Properties
    public float AttackDist
    {
        get { return attackDist; }
    }
}
