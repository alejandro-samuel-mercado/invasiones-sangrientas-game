using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
        else
        {
            Debug.LogError("No AudioSource assigned to AudioManager");
        }
    }
}
