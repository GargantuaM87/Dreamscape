using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject Irene;
    [SerializeField] float moveSpeed;
    [SerializeField] Slider dashSlider;
    private SkinnedMeshRenderer[] smr;

    private float horizontalInput;
    private float verticalInput;
    private Animator animator;
    private float timer;
#region Movement Variables
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
    private Rigidbody rb;
    #endregion Movement Variables
    #endregion Variables
    #region Monobehaviour
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        smr = GetComponentsInChildren<SkinnedMeshRenderer>();
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        dashes = consecutiveDashes;
        dashSlider.maxValue = dashCoolDown;
        dashSlider.value = dashCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        timer -= Time.deltaTime;

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;

        dashSlider.value = dashCoolDownTimer;

        if (Input.GetKey(KeyCode.Q) && dashCoolDownTimer < 0)
            DashAbility();

        if (iFrames == true)
            bc.enabled = false;
        else
            bc.enabled = true;

        if (Input.GetMouseButtonDown(0))
            FaceMouseOnClick();

        HandleDirection();
        Switch();
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
            dashSlider.value = dashCoolDownTimer;
        }
    }

    public void HandleDirection()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
            animator.SetFloat("Speed", 1);
        else
            animator.SetFloat("Speed", 0);

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    public void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (dashTime > 0)
        {
            /* Vector3 move = dashSpeed * Time.deltaTime * new Vector3(horizontalInput, 0, verticalInput);
             rb.MovePosition(rb.position + move);*/
            rb.linearVelocity = dashSpeed * Time.fixedDeltaTime * transform.forward;
        }

        else
        {
            /*Vector3 move = Time.deltaTime * new Vector3(horizontalInput, 0, verticalInput);
            rb.MovePosition(rb.position + move);*/
            rb.linearVelocity = moveSpeed * Time.fixedDeltaTime * moveDirection;
        }
    }

    public void FaceMouseOnClick()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit RayHit))
        {
            Vector3 Hitpoint = RayHit.point;
            Vector3 direction = (Hitpoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;
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
