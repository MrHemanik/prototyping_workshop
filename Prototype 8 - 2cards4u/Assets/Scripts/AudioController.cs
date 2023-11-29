using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip start;
    public AudioClip middle;
    public AudioClip end;
    private AudioSource _audioSource;
    public int currentAudio = 0;
    public float[] pitches = {0.90f, 1f, 1.10f};
    private Coroutine _coroutine;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    public void StartAudio(int pitch)
    
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(InternalStartAudio(pitch));
    }

    public IEnumerator InternalStartAudio(int pitch)
    {
        _audioSource.Stop();
        _audioSource.pitch = pitches[pitch];
        _audioSource.clip = start;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        _audioSource.loop = true;
        _audioSource.clip = middle;
        _audioSource.Play();
    }
    public void StopAudio()
    {
        StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(InternalStopAudio());
    }
    public IEnumerator InternalStopAudio()
    {
        
        _audioSource.Stop();
        _audioSource.clip = end;
        _audioSource.loop = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        _audioSource.Stop();
    }

}
