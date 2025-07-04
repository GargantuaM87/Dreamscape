using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this.gameObject);
                }
            }
        }
    }
}
