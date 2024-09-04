using UnityEngine;

public interface IInteractable
{
    KeyCode ActionKey { get; }

    void Interact();
    void InitInteraction();


    void CancelInteraction();

}