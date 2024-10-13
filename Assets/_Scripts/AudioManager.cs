using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource _audioSource;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip) => _audioSource.PlayOneShot(clip);

}
