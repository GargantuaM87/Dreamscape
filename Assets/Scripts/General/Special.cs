using UnityEngine;

public abstract class Special : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    protected Animator animator;
    public abstract void Execute();
    public abstract void DeExecute();
}
