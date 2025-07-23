using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private int currentShield, maxShield;
    [SerializeField] private bool isDestroyed = false;
    public Slider shieldSlider;
    public UnityEvent<GameObject> OnHitReference, OnDeathReference;
    private Health health;

    void Awake()
    {
        shieldSlider.maxValue = maxShield;
        shieldSlider.value = maxShield;

        health = GetComponent<Health>();
    }

    public void ChangeShield(int shieldValue)
    {
        maxShield += shieldValue;
        currentShield += shieldValue;
    }

    void Update()
    {
        MaintainShield();
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDestroyed)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentShield -= amount;

        if (currentShield > 0)
        {
            OnHitReference?.Invoke(sender);
        }
        else
        {
            OnDeathReference?.Invoke(sender);
            isDestroyed = true;
            this.gameObject.SetActive(false);
        }
    }

    public void MaintainShield()
    {
        if (shieldSlider.value != currentShield)
            shieldSlider.value = currentShield;

        if (shieldSlider.maxValue != maxShield)
            shieldSlider.maxValue = maxShield;
    }


    public int CurrentShield { get { return currentShield; } set { currentShield = value; } }
    public int MaxShield { get { return maxShield; } set { maxShield = value; } }

}
