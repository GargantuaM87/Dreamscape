using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfferMenu : MonoBehaviour
{
    [SerializeField] private List<Button> offerButtons;
    private Bells bells;
    private int bellAmount;

    void Awake()
    {
        bells = FindAnyObjectByType<PlayerController>().GetComponent<Bells>();
    }
    void Start()
    {
        bellAmount = bells.BellNum;
        Debug.Log("enavke");
        foreach (var button in offerButtons)
        {
            if (Int32.Parse(button.name) >= bellAmount)
                button.enabled = false;
            else
                button.enabled = true;
        }
    }
}
