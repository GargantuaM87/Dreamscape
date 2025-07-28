using DG.Tweening;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField] private Substate substate;
    [SerializeField] private Transform finalEntry;
    [SerializeField] private TMP_Text attentionText;

    public string GetDiscription()
    {
        return "Limbo Gate";
    }

    public void Interact(GameObject sender)
    {
        attentionText.gameObject.SetActive(false);
        attentionText.alpha = 1f;

        if (substate.WavesCleared() == true)
        {
            substate.GenerateNextState();
            if (substate.NextState == null)
                sender.transform.position = finalEntry.position;
            else
                sender.transform.position = substate.NextState.Entry.position;

            LimboManager.instance.UpdateSubstateCount();
        }
        else
        {
            Camera.main.DOShakePosition(0.2f, 10f, 20, 90, true);
            attentionText.gameObject.SetActive(true);
            attentionText.DOFade(0f, 1f);
        }
    }
}
