using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip gameplayMusic;
    public AudioClip failMusic;
    public AudioClip successMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayGameplayMusic();
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFailMusic()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = failMusic;
        audioSource.Play();
    }

    public void PlaySuccessMusic()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = successMusic;
        audioSource.Play();
    }
}
