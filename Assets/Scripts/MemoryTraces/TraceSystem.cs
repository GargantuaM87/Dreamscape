using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TraceSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Traces> traces;
    [SerializeField] private GameObject traceMenu;
    [SerializeField] private GameObject confirmationMenu;
    [SerializeField] private TraceHolder[] traceCards;
    public GameObject bellTowerEffects;
    private float constructDelay = 1.2f;
    private float delayTimer;
    private bool constructReady = false;
    private Material mat;
    private List<Traces> availableTraces;
    private Dictionary<string, Traces> selectList;
    private Rarity rarityTest;
    private string firstTrace, secondTrace, thirdTrace;
    private Bells bellComp;
    private int bells;

    void Start()
    {
        selectList = new Dictionary<string, Traces>();
        foreach (Traces trace in traces)
        {
            selectList.Add(trace.Name, trace);
        }

        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
            constructReady = true;
    }

    public void Interact(GameObject sender)
    {
        bellComp = sender.GetComponent<Bells>();
        bells = bellComp.BellNum;
        if (bells >= 1)
            confirmationMenu.SetActive(true);
    }

    public string GetDiscription()
    {
        return "Bell Tower";
    }

    public IEnumerator DelayConstruct()
    {
        yield return new WaitForSeconds(1.5f);
        Construct();
        StopCoroutine(DelayConstruct());
    }

    public void Prepare(int num)
    {
        if (bells >= num)
        {
            constructReady = false;
            delayTimer = constructDelay;

            DramaEffects();
            confirmationMenu.SetActive(false);
            bellComp.SetBells(bells - num);
            StartCoroutine(DelayConstruct());
        }
    }

    public void Construct()
    {
        availableTraces = new List<Traces>();

        traceMenu.SetActive(true);

        int randomNum = UnityEngine.Random.Range(0, 101);
        if (randomNum >= 0 && randomNum <= 60)
            rarityTest = Rarity.COMMON;
        if (randomNum >= 60 && randomNum <= 80)
            rarityTest = Rarity.RARE;
        if (randomNum >= 80 && randomNum <= 95)
            rarityTest = Rarity.EPIC;
        if (randomNum >= 95 && randomNum <= 100)
            rarityTest = Rarity.LEGENDARY;


        foreach (var (key, value) in selectList)
        {
            if (selectList[key].tRarity == Rarity.COMMON)
            {
                availableTraces.Add(selectList[key]);

                if (availableTraces.Count == 1)
                    firstTrace = selectList[key].Name;
                else if (availableTraces.Count == 2)
                    secondTrace = selectList[key].Name;
                else if (availableTraces.Count == 3)
                {
                    thirdTrace = selectList[key].Name;
                }
            }
        }
        selectList.Remove(firstTrace);
        selectList.Remove(secondTrace);
        selectList.Remove(thirdTrace);

        Traces[] tempList = availableTraces.ToArray();

        for (int i = 0; i < tempList.Length; i++)
        {
            traceCards[i].Trace = tempList[i];
            traceCards[i].Assign();
        }
    }

    public void DramaEffects()
    {
        Color oColor = mat.color;
        bellTowerEffects.SetActive(true);
        FreezeFrame.instance.ShakeCamera(1.5f, 7f, 10, 90, true);
        AudioManager.instance.PlaySound("BellTowerChime");
        AudioManager.instance.PlaySound("BellTowerInit");

        var sequence = DOTween.Sequence();
        sequence.Append(mat.DOColor(Color.white, 0.2f));
        sequence.Append(mat.DOColor(oColor, 0.2f));
    }

    public void Deconstruct()
    {
        traceMenu.SetActive(false);
        bellTowerEffects.SetActive(false);

        Traces[] tempList = availableTraces.ToArray();
        for (int i = 0; i < tempList.Length; i++)
        {
            if (traceCards[i].IsSelected == false)
                selectList.Add(tempList[i].Name, tempList[i]);
        }
        availableTraces.Clear();

    }
}
