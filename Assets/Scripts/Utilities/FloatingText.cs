using UnityEngine;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;

    public void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOShakeRotation(2.5f, 5f, 5, 5));
        sequence.Append(transform.DOShakePosition(2.5f, 5f, 5, 10));

        Destroy(gameObject, 3f);
    }

}
