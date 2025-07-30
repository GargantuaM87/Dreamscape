using TMPro;
using UnityEngine;

public class TraceHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    private bool isSelected = false;
    private Traces trace;
    //when you come back work on these:
    //text color for rarity
//label that tells what rarity a trace is 
    public void Assign()
    {
        nameText.text = trace.Name;
        descText.text = trace.Desc;

        nameText.color = ColorName();
    }

    public void TriggerExecute()
    {
        isSelected = true;
        trace.Execute();
    }

    private Color ColorName()
    {
        if (trace.tRarity == Rarity.COMMON)
            return new Color(43f, 161f, 209f);
        return Color.white;
    }

    public Traces Trace
    {
        get { return trace; }
        set { trace = value; }
    }

    public bool IsSelected { get { return isSelected; } }
}
