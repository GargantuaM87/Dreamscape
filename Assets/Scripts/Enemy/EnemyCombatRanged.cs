using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyCombatRanged : EnemyCombat
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject spawnPoint;
    /*private IObjectPool<GameObject> objectPool;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultSize = 20;
    [SerializeField] private int maxSize = 120;*/

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.LookAt(player);
        timer -= Time.deltaTime;
        PrepareAttack();
    }
    public override void OnAttack()
    {
        // base.OnAttack();
        GameObject obj = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
        obj.GetComponent<Rigidbody>().AddForce(projectileSpeed * transform.forward, ForceMode.Impulse);
        //obj.GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(obj.transform.forward * projectileSpeed);
        Destroy(obj, 2f);
    }

}
