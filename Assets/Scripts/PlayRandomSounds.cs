using System.Collections;
using UnityEngine;

public class PlayRandomSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] mySounds;

    public float minDelay = 3f;
    public float maxDelay = 10f;

    private void Start()
    {
        StartCoroutine(PlaySoundsAtRandomIntervals());
    }

    private IEnumerator PlaySoundsAtRandomIntervals()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (mySounds.Length > 0)
            {
                AudioClip randomSound = mySounds[Random.Range(0, mySounds.Length)];

                float slowChance = 0.3f; // 30% of the time
                if (Random.value < slowChance)
                {
                    audioSource.pitch = Random.Range(0.6f, 0.85f);
                }
                else
                {
                    audioSource.pitch = Random.Range(0.9f, 1.1f);
                }

                audioSource.PlayOneShot(randomSound);
            }
        }
    }
}