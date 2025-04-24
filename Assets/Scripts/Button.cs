using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public ButtonPuzzle buttonPuzzle;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the VR hand or controller touches the button
        if (other.CompareTag("PlayerHand"))
        {
            buttonPuzzle.OnButtonPressed(this);  // Call the puzzle manager method
        }
    }
}