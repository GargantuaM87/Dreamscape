using UnityEngine;
using UnityEngine.UI;

public abstract class Special : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected Slider cooldownSlider;
    protected float cooldownTimer;
    protected Animator animator;
    public abstract void Execute();
    public abstract void DeExecute();
}
