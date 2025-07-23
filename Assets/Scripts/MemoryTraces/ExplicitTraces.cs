using UnityEngine;

[CreateAssetMenu(fileName = "Trace", menuName = "Scriptable Objects/ExplicitTrace")]
public class ExplicitTraces : Traces
{
    //Get references to player for attack stat, health 
    [Header("Health")]
    [SerializeField] private int hpMod;
    [SerializeField] private bool isHealthMod;
    [Header("Weapon Stats")]
    [SerializeField] private float atkMod;
    [SerializeField] private float knockbackMod;
    [SerializeField] private bool isWeaponMod;
    [Header("Bell Stats")]
    [SerializeField] private int bellChanceMod;
    [SerializeField] private int gainMod;
    [SerializeField] private int somniMergeMod;
    [SerializeField] private bool isBellMod;
    private Health playerHealth;
    private Weapons playerWeapon;
    private Bells playerBells;
    private PlayerController player;


    public override void Execute()
    {
        player = FindAnyObjectByType<PlayerController>();
        playerHealth = player.GetComponent<Health>();
        playerWeapon = player.GetComponentInChildren<Weapons>(true);
        playerBells = player.GetComponent<Bells>();
        //Health Mods
        if (isHealthMod)
        {
            playerHealth.MaxHealth += hpMod;
            playerHealth.CurrentHealth += hpMod;
        }
        //Weapon Mods
        if (isWeaponMod)
        {
            playerWeapon.Damage += Mathf.FloorToInt(playerWeapon.Damage * atkMod);
            playerWeapon.Knockback += Mathf.FloorToInt(playerWeapon.Knockback * knockbackMod);
        }
        //Bell Mods
        if (isBellMod)
        {
            playerBells.GainChance = bellChanceMod;
            playerBells.GainBells(gainMod);
            playerBells.ReduceSomniMerge(somniMergeMod);
        }
    }
}
