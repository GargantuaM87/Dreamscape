using Unity.Mathematics;
using UnityEngine;

public class ShieldSpecial : Special
{
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projSpeed;
    [SerializeField] private GameObject spawn;
    private bool executed = false;
    private Shield shieldComp;
    private Health health;
    private Rigidbody rb;
    private PlayerController playerController;
    void Start()
    {
        health = GetComponent<Health>();
        shieldComp = GetComponent<Shield>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        shieldComp.CurrentShield = 0;
    }
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && cooldownTimer <= 0)
            Execute();
        if (Input.GetKeyUp(KeyCode.Mouse1) && executed)
            DeExecute();
    }

    public override void Execute()
    {
        shield.SetActive(true);
        shieldComp.CurrentShield = shieldComp.MaxShield;
        executed = true;

        animator.SetTrigger("Block");

        playerController.enabled = false;
    }
    public override void DeExecute()
    {
        shield.SetActive(false);
        shieldComp.CurrentShield = 0;
        cooldownTimer = cooldown;
        executed = false;

        playerController.enabled = true;

        GameObject obj = Instantiate(projectile, spawn.transform.position, quaternion.identity, transform);
        obj.GetComponent<Rigidbody>().AddForce(projSpeed * transform.forward, ForceMode.Impulse);
        rb.AddForce(projSpeed * 2f * -transform.forward, ForceMode.Impulse);

        animator.SetTrigger("Rest");
    }
}
