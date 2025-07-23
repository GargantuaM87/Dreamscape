using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float comboDelay = 0.2f;

    private float numClicks = 0;
    private float lastClickTime = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
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
    }

    public void ComboAttack1Transition()
    {
        if (numClicks >= 2)
            animator.SetTrigger("Attack2");
    }

    public void ComboAttack2Transition()
    {
        if (numClicks >= 3)
            animator.SetTrigger("Attack3");
    }
}
