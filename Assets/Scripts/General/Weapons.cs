using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int criticalChance = 50;
    [SerializeField] private int criticalFactor = 1;
    [SerializeField] private int knockbackForce = 5;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] private bool isRanged;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != gameObject.layer)
        {
            Vector3 pos = collision.contacts[0].point;
            if (hitPrefab != null)
            {
                GameObject hitEffect = Instantiate(hitPrefab, pos, Quaternion.identity);
                Destroy(hitEffect, 0.5f);
            }
            criticalFactor = 1;

            Health health;
            if (health = collision.gameObject.GetComponent<Health>())
            {
                int rand = UnityEngine.Random.Range(0, criticalChance);
                if (rand == 1)
                    criticalFactor = 2;

                /* if (criticalFactor >= 2)
                     FreezeFrame.instance.Freeze();*/
                health.GetHit(damage * criticalFactor, transform.parent.gameObject);
            }

            if (isRanged)
                Destroy(this);


            IHitable hitable = collision.transform.GetComponent<IHitable>();
            hitable?.Execute(transform, knockbackForce);
        }
    }

    public int Damage { get { return damage; } set { damage = value; } }
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }
      public int Knockback { get { return knockbackForce; } set { knockbackForce = value; } }
}
