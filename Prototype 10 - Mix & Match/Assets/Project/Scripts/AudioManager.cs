using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] aus;


    public void PlayHoverSound(float pitch)
    {
        aus[0].pitch = pitch;
        aus[0].Play();
    }
    public void PlayTravelSound()
    {
        aus[1].Play();
    }
}
