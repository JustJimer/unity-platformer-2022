using UnityEngine;

public class SoundLogic : MonoBehaviour
{
    public static SoundLogic instance { get; private set; }
    private AudioSource source;

    private void Start()
    {

        source = GetComponent<AudioSource>();
        instance = this;
        //Keep this object even when we go to new scene
        /*        if (instance == null)
                {
                    instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                //Destroy duplicate gameobjects
                else if (instance != null && instance != this)
                    Destroy(gameObject);*/
    }
    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
