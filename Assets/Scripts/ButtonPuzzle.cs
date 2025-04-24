using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [Header("Puzzle Buttons")]
    public List<Button> buttons;  // Set these in the correct order in the Inspector
    private int currentStep = 0;

    [Header("Door Settings")]
    public GameObject door;       // Assign your door GameObject here
    public float openHeight = 3f; // How far up the door should move
    public float openSpeed = 2f;  // How fast the door moves

    private bool puzzleSolved = false;
    private Vector3 doorStartPos;
    private Vector3 doorEndPos;

    private void Start()
    {
        // Store original and target position of the door
        if (door != null)
        {
            doorStartPos = door.transform.position;
            doorEndPos = doorStartPos + Vector3.up * openHeight;
        }
    }

    public void OnButtonPressed(Button button)
    {
        if (button == buttons[currentStep])
        {
            currentStep++;
            Debug.Log("Correct button!");

            if (currentStep >= buttons.Count)
            {
                Debug.Log("Puzzle Complete!");
                puzzleSolved = true;
            }
        }
        else
        {
            Debug.Log("Incorrect button. Resetting.");
            currentStep = 0;
        }
    }

    private void Update()
    {
        if (puzzleSolved && door != null)
        {
            door.transform.position = Vector3.MoveTowards(
                door.transform.position,
                doorEndPos,
                openSpeed * Time.deltaTime
            );
        }
    }
}