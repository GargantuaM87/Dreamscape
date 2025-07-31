using UnityEngine;

public class BellTower : MonoBehaviour, IInteractable
{
    public GameObject bellTowerEffects;
    private Bells bellComp;
    private int bells;
    private TraceSystem traceSystem;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    public void Interact(GameObject sender)
    {
        bellComp = sender.GetComponent<Bells>();
        bells = bellComp.BellNum;
        if (bells >= 1)
        {
            TraceSystem.instance.Confirmation();
            TraceSystem.instance.RecieveTowerData(bellComp, bells, this);
        }
    }

    public string GetDiscription()
    {
        return "Bell Tower";
    }

    public Material Mat {get { return mat; } set { mat = value; }}
}
