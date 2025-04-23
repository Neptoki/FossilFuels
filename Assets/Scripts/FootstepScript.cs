using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public Transform xrOriginTransform;
    public float movementThreshold = 0.01f;
    public float stepInterval = 0.5f;

    public AudioSource leftFootAudio;
    public AudioSource rightFootAudio;

    public List<AudioClip> dirtFootsteps; 
    public List<AudioClip> metalFootsteps;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    public float raycastDistance = 1.5f;
    private Vector3 lastPosition;
    private float stepTimer = 0f;
    private bool isLeftFoot = true;

    void Start()
    {
        if (xrOriginTransform == null || leftFootAudio == null || rightFootAudio == null)
        {
            Debug.LogWarning("FootstepScript missing references.");
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

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }

        lastPosition = xrOriginTransform.position;
    }

    void PlayFootstep()
    {
        AudioSource currentFoot = isLeftFoot ? leftFootAudio : rightFootAudio;
        currentFoot.pitch = Random.Range(minPitch, maxPitch);

        // Detect the surface with raycast
        string surfaceTag = GetSurfaceTag();

        // Always play a sound, but if it's metal, change to metal sound
        AudioClip clipToPlay = GetRandomClipForSurface(surfaceTag);
        if (clipToPlay != null)
        {
            currentFoot.clip = clipToPlay;
            currentFoot.Play();
        }

        isLeftFoot = !isLeftFoot;
    }

    string GetSurfaceTag()
    {
        RaycastHit hit;
        Vector3 rayOrigin = xrOriginTransform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            // Return "Metal" if metal is detected, otherwise return "Dirt"
            if (hit.collider.tag == "Metal")
            {
                return "Metal";
            }
            else if (hit.collider.tag == "Dirt")
            {
                return "Dirt";
            }
        }

        return "Untagged";
    }

    AudioClip GetRandomClipForSurface(string tag)
    {
        if (tag == "Metal" && metalFootsteps.Count > 0)
        {
            return metalFootsteps[Random.Range(0, metalFootsteps.Count)];
        }
        else if (tag == "Dirt" && dirtFootsteps.Count > 0)
        {
            return dirtFootsteps[Random.Range(0, dirtFootsteps.Count)];
        }

        // Fallback to dirt if no tag found
        return dirtFootsteps.Count > 0 ? dirtFootsteps[Random.Range(0, dirtFootsteps.Count)] : null;
    }
}
