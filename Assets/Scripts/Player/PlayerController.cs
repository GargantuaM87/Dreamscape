using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject Irene;
    [SerializeField] float moveSpeed;
    private SkinnedMeshRenderer[] smr;

    private float horizontalInput;
    private float verticalInput;
    private Animator animator;
    private float timer;

    [Header("Dash Parameters")]
    [SerializeField] private GameObject dashBody;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private int consecutiveDashes = 2;
    public int dashes;
    private float dashCoolDownTimer;
    private float dashTime;
    private BoxCollider bc;
    private bool iFrames = false;
    #endregion
    #region Monobehaviour
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        smr = GetComponentsInChildren<SkinnedMeshRenderer>();
        bc = GetComponent<BoxCollider>();
        dashes = consecutiveDashes;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        timer -= Time.deltaTime;

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Q) && dashCoolDownTimer < 0)
            DashAbility();

        if (iFrames == true)
            bc.enabled = false;
        else
            bc.enabled = true;

        Movement();
        Switch();
    }
    #endregion

    public void DashAbility()
    {
        dashTime = dashDuration;
        dashes -= 1;
        if (dashes <= 0)
        {
            dashes = consecutiveDashes;
            dashCoolDownTimer = dashCoolDown;
        }
    }

    public void Movement()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
            animator.SetFloat("Speed", 1);
        else
            animator.SetFloat("Speed", 0);

        if (dashTime > 0)
            transform.position += dashSpeed * Time.deltaTime * transform.forward;
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
            iFrames = true;

            foreach (var sm in smr)
                sm.enabled = false;
        }
        else
        {
            iFrames = false;
            dashBody.SetActive(false);
            foreach (var sm in smr)
                sm.enabled = true;
        }
    }
}
