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

    public AudioClip defaultFootstepClip;
    public AudioClip metalFootstepClip;
    public float raycastDistance = 1.5f; // Adjust based on height

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
                UpdateFootstepClip(); // Set the correct clip based on surface
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

    void UpdateFootstepClip()
    {
        RaycastHit hit;
        Vector3 rayOrigin = xrOriginTransform.position + Vector3.up * 0.1f; // Small offset to avoid self-collision

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Metal"))
            {
                footstepAudio.clip = metalFootstepClip;
            }
            else
            {
                footstepAudio.clip = defaultFootstepClip;
            }
        }
        else
        {
            // If nothing hit, fallback to default
            footstepAudio.clip = defaultFootstepClip;
        }
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
