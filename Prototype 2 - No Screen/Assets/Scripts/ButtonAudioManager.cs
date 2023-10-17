using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonAudioManager : MonoBehaviour
{
    public AudioManager bam;

    private static ButtonAudioManager Instance { get; set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this);
        } 
    }
    
    public void OnZero(InputAction.CallbackContext context)
    {
        bam.Play("0");
    }
    public void OnOne(InputAction.CallbackContext context)
    {
        bam.Play("1");
    }
    public void OnTwo(InputAction.CallbackContext context)
    {
        bam.Play("2");
    }
    public void OnThree(InputAction.CallbackContext context)
    {
        bam.Play("3");
    }
    public void OnFour(InputAction.CallbackContext context)
    {
        bam.Play("4");
    }
    public void OnFive(InputAction.CallbackContext context)
    {
        bam.Play("5");
    }
    public void OnSix(InputAction.CallbackContext context)
    {
        bam.Play("6");
    }
    public void OnSeven(InputAction.CallbackContext context)
    {
        bam.Play("7");
    }
    public void OnEight(InputAction.CallbackContext context)
    {
        bam.Play("8");
    }
    public void OnNine(InputAction.CallbackContext context)
    {
        bam.Play("9");
    }
    
}
