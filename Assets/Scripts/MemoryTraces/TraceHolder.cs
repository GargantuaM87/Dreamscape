using TMPro;
using UnityEngine;

public class TraceHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    private bool isSelected = false;
    private Traces trace;

    public void Assign()
    {
        nameText.text = trace.Name;
        descText.text = trace.Desc;
    }

    public void TriggerExecute()
    {
        isSelected = true;
        trace.Execute();
    }

    public Traces Trace
    {
        get { return trace; }
        set { trace = value; }
    }

     public bool IsSelected { get { return isSelected; } }
}
