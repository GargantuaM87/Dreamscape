using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject Irene;
    [SerializeField] float moveSpeed;

    private float horizontalInput;
    private float verticalInput;
    private Animator animator;
    private float timer;

    [Header("Dash Parameters")]
    [SerializeField] private GameObject dashBody;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    private float dashCoolDownTimer;
    #endregion
    #region Monobehaviour
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        timer -= Time.deltaTime;

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            DashAbility();

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
        }

        Movement();
        Switch();
    }
    #endregion

    public void DashAbility()
    {
        if (dashCoolDownTimer < 0)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }

    public void Movement()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (dashTime > 0)
            transform.position += dashSpeed * Time.deltaTime * moveDirection;
        else
            transform.position += moveSpeed * Time.deltaTime * moveDirection;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    public void Switch()
    {
        if (dashTime > 0)
        {
            dashBody.SetActive(true);
            Irene.SetActive(false);
        }
        else
        {
            dashBody.SetActive(false);
            Irene.SetActive(true);
        }

    }

}
