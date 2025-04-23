using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject footstep;
    public Transform xrOriginTransform; 
    public float movementThreshold = 0.01f;

    private Vector3 lastPosition;

    void Start()
    {
        footstep.SetActive(false);
        if (xrOriginTransform == null)
        {
            Debug.LogWarning("XR Origin Transform not assigned!");
            enabled = false;
            return;
        }
        lastPosition = xrOriginTransform.position;
    }

    void Update()
    {
        float movement = Vector3.Distance(xrOriginTransform.position, lastPosition);

        if (movement > movementThreshold)
        {
            footsteps();
        }
        else
        {
            StopFootsteps();
        }

        lastPosition = xrOriginTransform.position;
    }

    void footsteps()
    {
        footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
    }
}