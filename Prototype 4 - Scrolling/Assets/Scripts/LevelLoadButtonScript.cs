using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadButtonScript : MonoBehaviour
{
    public LevelManager lm;
    private AudioSource _audSou;

    public void Start()
    {
        _audSou = lm.gameObject.GetComponent<AudioSource>();
    }

    public void Restart(float speedMultiplier)
    {
        _audSou.Play();
        lm.LoadLevel(lm.GetCurrentLevel(),speedMultiplier,1f);
        
    }
    public void NextLevel(float speedMultiplier)
    {
        _audSou.Play();
        lm.LoadLevel(lm.GetCurrentLevel()+1,speedMultiplier,1f);
    }
    public void LoadLevelSlow(int level)
    {
        _audSou.Play();
        lm.LoadLevel(level,0.8f,1f);
    }
    public void LoadLevelNormal(int level)
    {
        _audSou.Play();
        lm.LoadLevel(level,1f,1f);
    }
    public void LoadLevelFast(int level)
    {
        _audSou.Play();
        lm.LoadLevel(level,1.5f,1f);
    }
    public void LoadLevelDouble(int level)
    {
        _audSou.Play();
        lm.LoadLevel(level,2f,1f);
    }
    public void MainMenu()
    {
        _audSou.Play();
        lm.OpenMainMenu();
    }
}
