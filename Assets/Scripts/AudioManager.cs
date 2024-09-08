using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // Populate this array in the Inspector

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1) // Make sure to use the correct tag
        {
            Debug.Log("played");
            PlayRandomAudio();
        }

        else if (other.gameObject.CompareTag("Booger"))
        {
            Debug.Log("played");
            PlayRandomAudio();
            // Destroy object after playing the sound
            StartCoroutine(DestroyAfterSound());
        }
    }

    public void PlayRandomAudio()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = audioSource.clip;
        tempAudioSource.Play();

        // Detach the audio source from the object so it can continue playing
        Destroy(gameObject); // Destroy the game object but not the audio source

        // Wait until the audio has finished playing before destroying the AudioSource
        yield return new WaitForSeconds(tempAudioSource.clip.length);
        Destroy(tempAudioSource);
    }
}