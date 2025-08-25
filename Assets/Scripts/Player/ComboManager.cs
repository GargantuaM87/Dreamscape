using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float comboDelay = 0.25f;
    [SerializeField] private float attackDelay = 0.15f;
    private float comboTimer;
    private float attackTimer;

    private float numClicks = 0;
    private float lastClickTime = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*void Update()
    {
        if (Time.time - lastClickTime > comboDelay)
            numClicks = 0;
        if (Input.GetMouseButtonDown(0))
        {
            lastClickTime = Time.time;
            numClicks++;

            if (numClicks == 1)
            {
                animator.SetTrigger("Attack1");
            }
            numClicks = Mathf.Clamp(numClicks, 0, 3);
        }
    }*/

    void Update()
    {
        attackTimer -= Time.deltaTime;
        comboTimer -= Time.deltaTime;

        if (Time.time - lastClickTime > attackDelay + 0.1f)
            numClicks = 0;

        if (Input.GetMouseButtonDown(0) && attackTimer <= 0)
        {
            lastClickTime = Time.time;
            numClicks++;
            attackTimer = attackDelay;

            if (numClicks == 1)
            {
                animator.SetTrigger("Attack1");
            }

            if (comboTimer <= 0)
            {
                if (numClicks >= 2)
                    animator.SetTrigger("Attack2");

                if (numClicks >= 3)
                    animator.SetTrigger("Attack3");

                comboTimer = comboDelay;
            }
            numClicks = Mathf.Clamp(numClicks, 0, 3);
        }
    }

    /* public void ComboAttack1Transition()
     {
         if (numClicks >= 2)
             animator.SetTrigger("Attack2");
     }

     public void ComboAttack2Transition()
     {
         if (numClicks >= 3)
             animator.SetTrigger("Attack3");
     }*/
}
