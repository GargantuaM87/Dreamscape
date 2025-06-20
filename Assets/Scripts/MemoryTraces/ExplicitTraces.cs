using UnityEngine;

[CreateAssetMenu(fileName = "Trace", menuName = "Scriptable Objects/ExplicitTrace")]
public class ExplicitTraces : Traces
{
    //Get references to player for attack stat, health 
    [SerializeField] private int healthModifier;
    [SerializeField] private int attackModifier;
    private Health playerHealth;
    private Weapons playerWeapon;


    public override void Execute()
    {
        playerHealth = FindAnyObjectByType<PlayerController>().GetComponentInChildren<Health>();
        playerWeapon = FindAnyObjectByType<PlayerController>().GetComponentInChildren<Weapons>(true);

        playerHealth.MaxHealth += healthModifier;
        playerHealth.CurrentHealth += healthModifier;
        playerWeapon.Damage += attackModifier;
    }
}
