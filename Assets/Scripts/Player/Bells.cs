using TMPro;
using UnityEngine;

public class Bells : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bellCount, somniCount;
    [SerializeField] private int gainChance;
    [SerializeField] private int bellGainAmount = 1;
    [SerializeField] private int somniMergeAmount = 10;
    [SerializeField] private int bells = 0;
    private int somni;

    void Update()
    {
        bellCount.text = " " + bells;
    }

    public void CalculateChance()
    {
        int randomNum = UnityEngine.Random.Range(0, gainChance);
        if (randomNum == 1)
        {
            bells += bellGainAmount;
            bellCount.text = " " + bells;
        }
    }

    public void GainBells(int num)
    {
        bells += num;
        bellCount.text = " " + bells;
    }

    public void GainSomni()
    {
        somni += 1;
        somniCount.text = "" + somni;
        if (somni == somniMergeAmount)
        {
            CalculateChance();
            somni = 0;
            somniCount.text = "" + somni;
        }
    }

    public void ReduceSomniMerge(int reduction)
    {
        somniMergeAmount -= reduction;
        if (somni >= somniMergeAmount)
        {
            int quotient = somni / somniMergeAmount;
            if (quotient >= 1)
                for (int i = 0; i < quotient; i++)
                    CalculateChance();
        
            somni %= somniMergeAmount;
        }
    }

    public void SetBells(int num)
    {
        bells = num;
    }

    public int BellNum { get { return bells; } }
    public int GainChance { get { return gainChance; } set { gainChance = value; } }
}
