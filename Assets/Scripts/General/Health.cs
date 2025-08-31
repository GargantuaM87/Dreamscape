using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;
    [SerializeField] private GameObject floatingText;
    [SerializeField] private bool isDead = false;
    public UnityEvent<GameObject> OnHitReference, OnDeathReference;

    public void ChangeHealth(int healthValue)
    {
        maxHealth += healthValue;
        currentHealth += healthValue;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (floatingText != null)
            ShowDamage(amount);

        if (currentHealth > 0)
        {
            OnHitReference?.Invoke(sender);
        }
        else
        {
            OnDeathReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void ShowDamage(int damage)
    {
        // GameObject damageIndicator = objectPooler.SpawnFromPool("FloatingText", transform.position, Quaternion.identity);
        GameObject damageIndicator = Instantiate(floatingText, transform.position, Quaternion.identity);
        damageIndicator.GetComponent<TextMeshPro>().text = " " + damage;
    }

    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
}
