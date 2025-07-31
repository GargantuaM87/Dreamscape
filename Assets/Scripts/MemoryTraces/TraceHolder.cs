using TMPro;
using UnityEngine;

public class TraceHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private TMP_Text rarityText;
    private bool isSelected = false;
    private Traces trace;
    //when you come back work on these:
    //text color for rarity
    //label that tells what rarity a trace is 
    public void Assign()
    {
        nameText.text = trace.Name;
        descText.text = trace.Desc;
        SetRarity();
    }

    public void TriggerExecute()
    {
        isSelected = true;
        trace.Execute();
    }

    private void SetRarity()
    {
        if (trace.tRarity == Rarity.COMMON)
            rarityText.text = "COMMON";
        if (trace.tRarity == Rarity.RARE)
            rarityText.text = "RARE";
        if (trace.tRarity == Rarity.EPIC)
            rarityText.text = "EPIC";
        if (trace.tRarity == Rarity.LEGENDARY)
            rarityText.text = "LEGENDARY";
    }

    public Traces Trace
    {
        get { return trace; }
        set { trace = value; }
    }

    public bool IsSelected { get { return isSelected; } }
}
