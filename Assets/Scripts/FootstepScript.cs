using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject footstep;
    public Transform xrOriginTransform;
    public float movementThreshold = 0.01f;
    public float stepInterval = 0.5f;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    private Vector3 lastPosition;
    private float stepTimer = 0f;
    private AudioSource footstepAudio;

    void Start()
    {
        footstep.SetActive(false);
        footstepAudio = footstep.GetComponent<AudioSource>();

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
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval && footstepAudio != null)
            {
                footstepAudio.pitch = Random.Range(minPitch, maxPitch);
                footstepAudio.Play();
                stepTimer = 0f;
            }

            footsteps();
        }
        else
        {
            StopFootsteps();
            stepTimer = 0f;
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