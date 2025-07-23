using System.Drawing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int criticalChance = 50;
    [SerializeField] private int criticalFactor = 1;
    [SerializeField] private int knockbackForce = 5;
    [SerializeField] GameObject hitPrefab;

    //Work on this later to derive a better formula for critical hits
    void OnCollisionEnter(Collision collision)
    {
        //For collisions on the same layer
        if (collision.gameObject.layer != gameObject.layer)
        {   //Instantiating hit effects
            Vector3 pos = collision.contacts[0].point;
            if (hitPrefab != null)
            {
                GameObject hitEffect = Instantiate(hitPrefab, pos, Quaternion.identity);
                Destroy(hitEffect, 0.5f);
            }
            criticalFactor = 1;
            //Critical logic
            int rand = UnityEngine.Random.Range(0, criticalChance);
            if (rand == 1)
                criticalFactor = 2;
            DamageCollision(collision);

            //Responsible for providing knockback to gameobjects
            IHitable hitable = collision.transform.GetComponent<IHitable>();
            hitable?.Execute(transform, knockbackForce);
        }
    }

    public void DamageCollision(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Shield>(out Shield shield))
            {
                if (shield.CurrentShield > 0)
                {
                    shield.GetHit(damage * criticalFactor, transform.parent.gameObject);
                    Debug.Log("Shield was hit!");
                    return;
                }
            }
            //If statement can only be reached if there is no viable shield on a gameobject
            if (collision.gameObject.TryGetComponent<Health>(out Health health))
                health.GetHit(damage * criticalFactor, transform.parent.gameObject);
    }
    public int Damage { get { return damage; } set { damage = value; } }
    public int CriticalChance { get { return criticalChance; } set { criticalChance = value; } }
    public int Knockback { get { return knockbackForce; } set { knockbackForce = value; } }
}
