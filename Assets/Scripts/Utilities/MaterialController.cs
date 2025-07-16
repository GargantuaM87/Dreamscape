using UnityEngine;
using DG.Tweening;

public class MaterialController : MonoBehaviour
{
    private Material mat1;
    private Material mat2;
    private Material mat3;

    public MaterialController(Material m1, Material m2, Material m3)
    {
        this.mat1 = m1;
        this.mat2 = m2;
        this.mat3 = m3;
    }

    public void DOColor(Color color)
    {
        Color originalColor = Color.black;

        var sequence = DOTween.Sequence();
        if (mat1 != null)
        {
            sequence.Append(mat1.DOColor(color, 0.2f));
            sequence.Append(mat1.DOColor(originalColor, 0.2f));
        }
        if (mat2 != null)
        {
            sequence.Append(mat2.DOColor(color, 0.2f));
            sequence.Append(mat2.DOColor(originalColor, 0.2f));
        }
        if (mat3 != null)
        {
            sequence.Append(mat3.DOColor(color, 0.2f));
            sequence.Append(mat3.DOColor(originalColor, 0.2f));
        }   
    }
}
