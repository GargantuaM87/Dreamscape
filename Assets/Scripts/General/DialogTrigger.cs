using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private List<dialogString> dialogStrings = new List<dialogString>();
    private bool hasSpoken = false;

    public string GetDiscription()
    {
        return "Speak";
    }

    public void Interact(GameObject sender)
    {
        DialogManager.instance.DialogStart(dialogStrings);
        Debug.Log(sender.name);
        Debug.Log("Works!");
        hasSpoken = true;
    }

}

[System.Serializable]

public class dialogString
{
    public string text; //Represents NPC text
    public bool isEnd; //Represent if the line is the final line for the convo

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Trigger Events")]
    public UnityEvent startDialogEvent;
    public UnityEvent endDialogEvent;
}
