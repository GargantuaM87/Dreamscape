using UnityEngine;

public class BellTower : MonoBehaviour, IInteractable
{
    public GameObject bellTowerEffects;
    private Bells bellComp;
    private int bells;
    private TraceSystem traceSystem;
    private Material mat;
    private GameObject player;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    public void Interact(GameObject sender)
    {
        player = sender;

        bellComp = sender.GetComponent<Bells>();
        bells = bellComp.BellNum;
        if (bells >= 1)
        {
            TraceSystem.instance.Confirmation();
            TraceSystem.instance.RecieveTowerData(bellComp, bells, this);
        }
    }

    public void PowerUpEffect()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z);
        GameObject effects = Instantiate(TraceSystem.instance.powerupEffects, newPos, Quaternion.identity, player.transform);
        Destroy(effects, 2f);
    }

    public string GetDiscription()
    {
        return "Bell Tower";
    }

    public Material Mat {get { return mat; } set { mat = value; }}
}
