using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimboManager : MonoBehaviour
{
    public static LimboManager instance;
    [Header("Available Substates")]
    [SerializeField] private List<Substate> data;
    [SerializeField] private Substate substateA;
    [Header("UI")]
    [SerializeField] private TMP_Text countText;
    private List<Substate> substates;
    private int substateCount = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        substates = data;

        int rNum = Random.Range(0, substates.Count + 1);
        GeneratePath(substateA, substates, rNum);

        substateA.GenerateEnemies();
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

    public void UpdateSubstateCount()
    {
        substateCount += 1;
        countText.text = "Substate - " + substateCount;
    }

}
