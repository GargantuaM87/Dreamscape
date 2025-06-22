using UnityEngine;

[CreateAssetMenu(fileName = "Trace", menuName = "Scriptable Objects/ExplicitTrace")]
public class ExplicitTraces : Traces
{
    //Get references to player for attack stat, health 
    [SerializeField] private int hpMod;
    [SerializeField] private int atkMod;
    [SerializeField] private float knockbackMod;
    private Health playerHealth;
    private Weapons playerWeapon;
    private PlayerController player;


    public override void Execute()
    {
        playerHealth = FindAnyObjectByType<PlayerController>().GetComponentInChildren<Health>();
        playerWeapon = FindAnyObjectByType<PlayerController>().GetComponentInChildren<Weapons>(true);

        playerHealth.MaxHealth += hpMod;
        playerHealth.CurrentHealth += hpMod;

        playerWeapon.Damage += atkMod;
        playerWeapon.Knockback = (int)(playerWeapon.Knockback * knockbackMod);
    }
}
