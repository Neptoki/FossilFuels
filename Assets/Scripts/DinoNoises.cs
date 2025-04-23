using UnityEngine;

public class DinoGrowlAndFootsteps : MonoBehaviour
{
    public Transform player;              
    public float growlDistance = 20f;       
    public float randomGrowlDistance = 30f;
    public float minGrowlInterval = 2f;     
    public float maxGrowlInterval = 5f;    
    public AudioClip[] growlClips;        
    public AudioClip footstepClip;         
    public float footstepInterval = 0.5f;  
    public float movementThreshold = 0.1f;
    public float footstepMaxDistance = 50f;

    private AudioSource growlAudio;        
    private float growlTimer = 0f;         
    private bool isGrowling = false;        
    private Vector3 lastPosition;          
    private float footstepTimer = 0f;       

    void Start()
    {
        growlAudio = GetComponent<AudioSource>();
        lastPosition = transform.position;
        growlTimer = Random.Range(minGrowlInterval, maxGrowlInterval);

        SetupFootstepAudio();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= growlDistance)
        {
            if (!growlAudio.isPlaying && !isGrowling)
            {
                growlAudio.Play();
                isGrowling = true;
            }
        }
        else
        {
            if (growlAudio.isPlaying && isGrowling)
            {
                growlAudio.Stop();
                isGrowling = false;
            }
        }

        if (distance > randomGrowlDistance)
        {
            growlTimer -= Time.deltaTime;
            if (growlTimer <= 0f)
            {
                PlayRandomGrowl();
                growlTimer = Random.Range(minGrowlInterval, maxGrowlInterval); 
            }
        }

        HandleFootsteps();
    }

    private void PlayRandomGrowl()
    {
        if (growlClips.Length > 0)
        {
            AudioClip randomGrowl = growlClips[Random.Range(0, growlClips.Length)];

            growlAudio.pitch = Random.Range(0.8f, 1.2f); 

            AudioSource.PlayClipAtPoint(randomGrowl, transform.position, 1f); 
        }
    }

    private void HandleFootsteps()
    {
        if (Vector3.Distance(transform.position, lastPosition) > movementThreshold)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval; 
            }
        }
        lastPosition = transform.position;
    }

    private void PlayFootstep()
    {
        if (footstepClip != null)
        {
            AudioSource.PlayClipAtPoint(footstepClip, transform.position);
        }
    }

    private void SetupFootstepAudio()
    {
        AudioSource footstepSource = gameObject.AddComponent<AudioSource>();

        footstepSource.clip = footstepClip;
        footstepSource.spatialBlend = 1.0f; 
        footstepSource.minDistance = 5f;    
        footstepSource.maxDistance = footstepMaxDistance;
        footstepSource.rolloffMode = AudioRolloffMode.Linear; 
        footstepSource.volume = 1.0f;     
    }
}