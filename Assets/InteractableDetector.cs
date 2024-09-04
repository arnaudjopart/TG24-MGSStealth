using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    [SerializeField] private MGSInputController m_controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null) m_controller.SetInteractable(interactable);
        
    }

    private void OnTriggerExit(Collider other)
    {
        m_controller.ReleaseInteractable();
    }
}
