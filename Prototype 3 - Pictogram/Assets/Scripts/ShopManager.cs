using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameManager gm;
    public NumberVisualizer nvClickUpgrade;
    public NumberVisualizer nvAutoClickUpgrade;
    public NumberVisualizer nvAutoSpeedUpgrade;
    private (int, int)[] _clickUpgrades = new (int, int)[]
    {
        (20, 2), //Costs 20 Hearts, sets clickAmount to 2
        (100, 3),
        (400, 5),
        (1000, 8),
        (10000, 10)
    };
    private (int, int)[] _autoClickUpgrades = new (int, int)[]
    {
        (20, 1), //Costs 20 Hearts, sets AutoClickAmount to 1
        (100, 2),
        (300, 3),
        (1000, 4),
        (5000, 7),
        (10000, 10)
    };
    private (int, int)[] _autoSpeedUpgrades = new (int, int)[]
    {
        (100, 1500), //Costs 100 Hearts, sets autoClickSpeed to 1500ms
        (300, 1000), 
        (500, 800),
        (1000, 700),
        (2000, 500),
        (5000, 200),
        (10000, 100)
    };
    private int _currentBoughtClickUpgrade = 0;
    private int _currentBoughtAutoClickUpgrade = 0;
    private int _currentBoughtAutoSpeedUpgrade = 0;

    private void Start()
    {
        nvClickUpgrade.VisualizeHearts(_clickUpgrades[_currentBoughtClickUpgrade].Item1);
        nvAutoClickUpgrade.VisualizeHearts(_autoClickUpgrades[_currentBoughtAutoClickUpgrade].Item1);
        nvAutoSpeedUpgrade.VisualizeHearts(_autoSpeedUpgrades[_currentBoughtAutoSpeedUpgrade].Item1);
    }

    public void CheckClickUpgrade()
    {
        var curUpgrade = _clickUpgrades[_currentBoughtClickUpgrade];
        if (curUpgrade.Item1 <= gm.hearts)
        {
            NextClickUpgrade();
            gm.AddClickUpgrade(curUpgrade.Item1, curUpgrade.Item2);
        }
        else
        {
            PlayNotEnoughSound();
        }
    }

    private void NextClickUpgrade()
    {
        _currentBoughtClickUpgrade++;
        nvClickUpgrade.VisualizeHearts(_clickUpgrades[_currentBoughtClickUpgrade].Item1);
    }
    
    public void CheckAutoClickUpgrade()
    {
        var curUpgrade = _autoClickUpgrades[_currentBoughtAutoClickUpgrade];
        if (curUpgrade.Item1 <= gm.hearts)
        {
            NextAutoClickUpgrade();
            gm.AddAutoClickUpgrade(curUpgrade.Item1, curUpgrade.Item2);
        }
        else
        {
            PlayNotEnoughSound();
        }
    }

    private void NextAutoClickUpgrade()
    {
        _currentBoughtAutoClickUpgrade++;
        nvAutoClickUpgrade.VisualizeHearts(_autoClickUpgrades[_currentBoughtAutoClickUpgrade].Item1);
    }
    
    public void CheckAutoSpeedUpgrade()
    {
        var curUpgrade = _autoSpeedUpgrades[_currentBoughtAutoSpeedUpgrade];
        if (curUpgrade.Item1 <= gm.hearts)
        {
            NextAutoSpeedUpgrade();
            gm.AddAutoSpeedUpgrade(curUpgrade.Item1, curUpgrade.Item2);
        }
        else
        {
            PlayNotEnoughSound();
        }
    }

    private void NextAutoSpeedUpgrade()
    {
        _currentBoughtAutoSpeedUpgrade++;
        nvAutoSpeedUpgrade.VisualizeHearts(_autoSpeedUpgrades[_currentBoughtAutoSpeedUpgrade].Item1);
    }
    
    
    

    private void PlayNotEnoughSound()
    {
        //Play sound that you cant buy
    }
    
    
}
