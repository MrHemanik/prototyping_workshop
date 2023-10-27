using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Copied mostly from https://www.youtube.com/watch?v=GqHFGMy_51c
public class Oscillator : MonoBehaviour
{
    public float frequency = 440;
    private float _increment;
    private float _phase;
    private float sampling_frequency = 48000;
    [Range(0.0f,0.1f)] public float _gain = 0 ;
    private bool _audioStyle = true; //False Sinus, True Square
    private float _defaultGain = 0.02f;

    //https://pages.mtu.edu/~suits/notefreqs.html
    [NonSerialized] private float[] frequencies = new[]
    {
        440.00f/4, // A2
        493.88f/4, // B2
        523.25f/4, // C3
        587.33f/4, // D3
        659.25f/4, // E3
        698.46f/4, // F3
        783.99f/4, // G3
        440.00f/2, // A3
        493.88f/2, // B3
        523.25f/2, // C4
        587.33f/2, // D4
        659.25f/2, // E4
        698.46f/2, // F4
        783.99f/2, // G4
        440.00f, // A4
        493.88f, // B4
        523.25f, // C5
        587.33f, // D5
        659.25f, // E5
        698.46f, // F5
        783.99f, // G5
        880.00f, // A5
        987.77f, // B5
        1046.50f, // C6
        1174.66f, // D6
        1318.51f, // E6
        1396.91f, // F6
        1567.98f, // G6
        
    };

    public void SetFrequencyByNoteID(int id)
    {
        frequency = frequencies[id];
        _gain = _defaultGain;
    }

    public void StopSound()
    {
        _gain = 0;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        _increment = frequency * 2.0f * Mathf.PI / sampling_frequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            _phase += _increment;
            if (!_audioStyle) data[i] = _gain * Mathf.Sin(_phase);
            else if (_gain * Mathf.Sin(_phase) >= 0) data[i] = (_gain * Mathf.Sin(_phase) >= 0 ? _gain : -_gain) * 0.6f;
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (_phase > Math.PI * 2)
            {
                _phase = 0f;
            }
        }
    }
}