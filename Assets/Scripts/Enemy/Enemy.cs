using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable
{
    private AudioManager audioManager;
    private Rigidbody rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody>();
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
    public void HitSplash()
    {
        audioManager.PlaySound("HitEffect");
        DOTween.KillAll(true);
        transform.DOShakeScale(0.4f, 0.5f, 1, 2, true, ShakeRandomnessMode.Harmonic);
    }

    public void Execute(Transform executeSource, int knockbackForce)
    {
        KnockbackEntity(executeSource, knockbackForce);
    }

    public void KnockbackEntity(Transform executionSource, int knockbackForce)
    {
        Vector3 dir = (transform.position - executionSource.transform.position).normalized;
        Vector3 pos = transform.position;
        pos.x += dir.x * knockbackForce;
        pos.z += dir.z * knockbackForce;

        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
}
