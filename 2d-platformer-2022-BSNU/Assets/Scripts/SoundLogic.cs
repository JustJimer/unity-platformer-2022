using UnityEngine;

public class SoundLogic : MonoBehaviour
{
    public static SoundLogic instance { get; private set; }
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
