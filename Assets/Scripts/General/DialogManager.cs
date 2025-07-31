using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using TMPro;
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
    private SpeakerInfo speakerInfo;
    [Header("Irene Info")]
    [SerializeField] private Sprite ireneAvatar;

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

        this.speakerInfo = speakerInfo;
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

            SetSpeakerInfo(speakerInfo);

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
                if (line.ireneMainDialog.Length > 0)
                    yield return StartCoroutine(IreneDialog(line.ireneMainDialog, false));
            }
            line.startDialogEvent?.Invoke();

            optionSelected = false;
        }
        DialogStop();
    }

    private IEnumerator IreneDialog(string ireneDialog, bool wasQuestion)
    {
        avatarDisplay.sprite = ireneAvatar;
        sName.text = "IRENE";
        sTitle.text = "DREAMER";

        yield return StartCoroutine(TypeIreneText(ireneDialog));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        optionSelected = wasQuestion;
    }
    public void ClickedButtonOne() => HandleOptionSelected(line.option1IndexJump, line.ireneDialog1);
    public void ClickedButtonTwo() => HandleOptionSelected(line.option2IndexJump, line.ireneDialog2);

    private void HandleOptionSelected(int indexJump, string ireneText)
    {
        StartCoroutine(IreneDialog(ireneText, true));
        DisableButtons();
        currentDialogIndex = indexJump;
    }

    private IEnumerator TypeIreneText(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
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
