using UnityEngine;

public interface IHitable
{
    public void Execute(Transform executeSource, int knockbackForce);
}
