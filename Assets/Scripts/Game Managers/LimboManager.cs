using System.Collections.Generic;
using UnityEngine;

public class LimboManager : MonoBehaviour
{
    [Header("Available Substates")]
    [SerializeField] private List<Substate> data;
    [SerializeField] private Substate substateA;
    private List<Substate> substates;

    void Start()
    {
        substates = data;

        int rNum = Random.Range(0, substates.Count + 1);
        GeneratePath(substateA, substates, rNum);
    }

    public void GeneratePath(Substate substate, List<Substate> items, int paths)
    {
        if (paths <= 0)
            return;

        int rNum = Random.Range(0, substates.Count);
        Substate pointer = substates[rNum];
        items.Remove(items[rNum]);
        substate.NextState = pointer;
        Debug.Log(substate.NextState);
        GeneratePath(pointer, items, paths - 1);
    }

}
