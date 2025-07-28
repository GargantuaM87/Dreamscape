using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using TMPro;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine.Animations;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [Header("Dialogue Info")]
    [SerializeField] private GameObject dialogParent;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private float typingSpeed = 0.05f;
    private List<DialogString> dialogList;
    DialogString line;
    private RigidbodyConstraints originalConstraints;
    [Header("Speaker Info")]
    [SerializeField] private Image avatarDisplay;
    [SerializeField] private TMP_Text sName;
    [SerializeField] private TMP_Text sTitle;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    private int currentDialogIndex = 0;

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
    private void Start()
    {
        originalConstraints = playerController.GetComponent<Rigidbody>().constraints;
        dialogParent.SetActive(false);
    }

    public void DialogStart(List<DialogString> textToPrint, SpeakerInfo speakerInfo)
    {
        dialogParent.SetActive(true);
        playerController.enabled = false;
        playerController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        SetSpeakerInfo(speakerInfo);

        dialogList = textToPrint;
        currentDialogIndex = 0;

        DisableButtons();
        StartCoroutine(PrintDialog());
    }

    private void SetSpeakerInfo(SpeakerInfo speakerInfo)
    {
        avatarDisplay.sprite = speakerInfo.Avatar;
        sName.text = speakerInfo.SpeakerName;
        sTitle.text = speakerInfo.SpeakerTitle;
    }

    private void DisableButtons()
    {
        option1Button.GetComponentInChildren<TMP_Text>().text = "";
        option2Button.GetComponentInChildren<TMP_Text>().text = "";

        option1Button.transform.DOLocalMoveX(1400f, 1.2f);
        option2Button.transform.DOLocalMoveX(1400f, 1.2f);
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialog()
    {
        while (currentDialogIndex < dialogList.Count)
        {
            line = dialogList[currentDialogIndex];

            line.startDialogEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));


                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;

                option1Button.transform.DOLocalMoveX(650f, 1f);
                option2Button.transform.DOLocalMoveX(650f, 1f);


                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.startDialogEvent?.Invoke();

            optionSelected = false;
        }
        DialogStop();
    }

    public void ClickedButtonOne() => HandleOptionSelected(line.option1IndexJump);
    public void ClickedButtonTwo() => HandleOptionSelected(line.option2IndexJump);

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogList[currentDialogIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogList[currentDialogIndex].isEnd)
            DialogStop();

        currentDialogIndex++;
    }

    private void DialogStop()
    {
        StopAllCoroutines();
        dialogText.text = " ";
        dialogParent.SetActive(false);

        playerController.enabled = true;
        playerController.GetComponent<Rigidbody>().constraints = originalConstraints;
    }
}
