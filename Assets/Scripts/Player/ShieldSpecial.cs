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
    void Start()
    {
        health = GetComponent<Health>();
        shieldComp = GetComponent<Shield>();
        animator = GetComponent<Animator>();
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
    }
    public override void DeExecute()
    {
        shield.SetActive(false);
        shieldComp.CurrentShield = 0;
        cooldownTimer = cooldown;
        executed = false;

        GameObject obj = Instantiate(projectile, spawn.transform.position, quaternion.identity, transform);
        obj.GetComponent<Rigidbody>().AddForce(projSpeed * transform.forward, ForceMode.Impulse);
        Destroy(obj, 0.5f);
        
        animator.SetTrigger("Rest");
    }
}
