using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float maxDistance = 5;
    public LayerMask interactableLayers;

    private Interactable currentInteractable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactableLayers)) {
            currentInteractable = hit.collider.GetComponent<Interactable>();
        } else {
            currentInteractable = null;
        }
    }

    public void Interact(){
        if (currentInteractable) {
            currentInteractable.OnInteraction();
        }
    }

    public void OnMouseDown(){
        Interact();
        Debug.Log("down");
    }
}
