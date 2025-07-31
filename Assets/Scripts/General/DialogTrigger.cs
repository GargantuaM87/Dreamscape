using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private List<DialogString> dialogStrings = new List<DialogString>();
    [SerializeField] private SpeakerInfo speakerInfo;
    private bool hasSpoken = false;

    public string GetDiscription()
    {
        return "Speak";
    }

    public void Interact(GameObject sender)
    {
        DialogManager.instance.DialogStart(dialogStrings, speakerInfo);
        hasSpoken = true;
    }

}

[System.Serializable]

public class DialogString
{
    [TextArea]
    public string text; //Represents NPC text
    public bool isEnd; //Represent if the line is the final line for the convo

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;
    [Header("Irene Branch")]

    [TextArea] public string ireneMainDialog = null;
    [TextArea] public string ireneDialog1;
    [TextArea] public string ireneDialog2;

    [Header("Trigger Events")]
    public UnityEvent startDialogEvent;
    public UnityEvent endDialogEvent;
}

[System.Serializable]

public class SpeakerInfo
{
    [SerializeField]
    private Sprite avatar;
    [SerializeField] private string speakerName;
    [SerializeField] private string speakerTitle;

    public Sprite Avatar { get { return avatar; } }
    public string SpeakerName { get { return speakerName; } }
    public string SpeakerTitle { get { return speakerTitle; } }
}

