using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] Material[] mats;
    [SerializeField] private float priority;
    private Waves parentWave;
    private MaterialController matController;
    private AudioManager audioManager;
    private Rigidbody rb;
    private Bells bellSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        matController = new MaterialController(mats[0], mats[1], mats[2]);
        bellSystem = FindAnyObjectByType<Bells>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PDamage"))
        {
            //Invoke...
            HitSplash();
            //Enemy burst after dying with screen shake
        }
    }
    /// <summary>
    /// Visual Effects for when an enemy is damaged.
    /// </summary>
    public void HitSplash()
    {
        audioManager.PlaySound("HitEffect");
        DOTween.KillAll(true);
        transform.DOShakeScale(0.4f, 0.5f, 1, 2, true, ShakeRandomnessMode.Harmonic);
        matController.DOColor(Color.white);
    }
    /// <summary>
    /// Called externally to provide information such as the force of knock back.
    /// </summary>
    /// <param name="executeSource"></param>
    /// <param name="knockbackForce"></param>
    public void Execute(Transform executeSource, int knockbackForce)
    {
        KnockbackEntity(executeSource, knockbackForce);
    }
    public void DecreaseWaveLength() => parentWave.EnemyLength -= 1;
    public void IncreaseSomni() => bellSystem.GainSomni();
    /// <summary>
    /// Calculations for executing knock back logic on an enemy through physics.
    /// </summary>
    /// <param name="executionSource"></param>
    /// <param name="knockbackForce"></param>
    public void KnockbackEntity(Transform executionSource, int knockbackForce)
    {
        Vector3 dir = (transform.position - executionSource.transform.position).normalized;
        //Vector3 pos = transform.position;
        Vector3 dirForce = new Vector3(dir.x, 0, dir.z);
        rb.MovePosition(rb.position + dirForce * knockbackForce);
        /* pos.x += dir.x * knockbackForce;
           pos.z += dir.z * knockbackForce;
         transform.position = new Vector3(pos.x, transform.position.y, pos.z);*/
    }
    public float Priority { get { return priority; } set { priority = value; } }
    public Waves ParentWave { get { return parentWave; } set { parentWave = value; } }
}
