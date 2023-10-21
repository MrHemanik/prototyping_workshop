using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; set; }
    public NumberVisualizer nv;
    public StatsVisualizer sv;
    public int hearts = 0;
    public int heartsPerClick = 1;
    public int heartsPerAutoClick = 0;
    public int heartsPerAutoClickInterval = 2000;
    public Coroutine _autoClickerCoroutine;
    public AudioSource autoClickAudioSource;
    public Animation phoneAnimation;
    public Animation autoClickAnimation;
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

    private void Start()
    {
        sv.UpdateHPC(heartsPerClick);
        sv.UpdateHPAC(heartsPerAutoClick);
        sv.UpdateHPACI(heartsPerAutoClickInterval);
    }

    public void AddHeartsPerClick()
    {
        hearts += heartsPerClick;
        nv.VisualizeHearts(hearts);
    }
    public void AddHeartsPerAutoClick()
    {
        hearts += heartsPerAutoClick;
        nv.VisualizeHearts(hearts);
    }

    private void RemoveHearts(int amount)
    {
        //needs to check before usage if amount < hearts
        hearts -= amount;
        nv.VisualizeHearts(hearts);
    }

    public void AddClickUpgrade(int price, int hpc)
    {
        RemoveHearts(price);
        heartsPerClick = hpc;
        sv.UpdateHPC(hpc);
    }
    public void AddAutoClickUpgrade(int price, int hpac)
    {
        if (_autoClickerCoroutine == null)
        {
            StartAutoClickerCoroutine();
            sv.VisualizeAutoStats();
        } 
        RemoveHearts(price);
        heartsPerAutoClick = hpac;
        sv.UpdateHPAC(hpac);
    }
    public void AddAutoSpeedUpgrade(int price, int hpaci)
    {
        RemoveHearts(price);
        heartsPerAutoClickInterval = hpaci;
        sv.UpdateHPACI(hpaci);
    }

    public void StartAutoClickerCoroutine()
    {
        Debug.Log("AutoClickerCoroutine Start");
        _autoClickerCoroutine = StartCoroutine(AutoClickerCoroutine());
    }

    private IEnumerator AutoClickerCoroutine()
    {
        while (true)
        {
            Debug.Log("AutoClickerCoroutine Click");
            yield return new WaitForSeconds(heartsPerAutoClickInterval/1000f);
            autoClickAnimation.Play();
            AddHeartsPerAutoClick();
            autoClickAudioSource.Play();
            phoneAnimation.Stop();
            phoneAnimation.Play();
            
        }
    }
}
