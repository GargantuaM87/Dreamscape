using DG.Tweening;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    public static FreezeFrame instance;
    [SerializeField] private float unfreezeTimer = 0.2f;
    private float timer;
    private float freezeCooldown = 10f;
    private float freezeTimer;
    private Camera mainCamera;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.unscaledDeltaTime;
        if (timer <= 0)
            Time.timeScale = 1;

        freezeTimer -= Time.deltaTime;
    }

    public void Freeze()
    {
        if (freezeTimer <= 0)
        {
            Time.timeScale = 0;
            timer = unfreezeTimer;
            freezeTimer = freezeCooldown;
        }
    }

    public void ShakeCamera(float duration, float strength, int vibrato, float randomness, bool fadeOut)
    {
        mainCamera.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    }
}
