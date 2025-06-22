using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] TextMeshProUGUI interactionText;
    private IInteractable interactable;

    void Start()
    {
        interactable = GetComponent<IInteractable>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            interactionUI.SetActive(true);
            interactionText.text = interactable.GetDiscription();
        }
    }

    void OnTriggerExit(Collider other)
    {
        interactionUI.SetActive(false);
    }
}
