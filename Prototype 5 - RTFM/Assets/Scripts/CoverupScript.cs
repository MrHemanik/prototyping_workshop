using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverupScript : MonoBehaviour
{
    public GameManager gm;

    public Image coverup;
    public GameObject[] screws;
    private int _activeScrews = 4;
    
    public void UnscrewScrew(int screw)
    {
        if (!gm.hasScrewdriver) return;
        screws[screw].SetActive(false);
        _activeScrews--;
        if (_activeScrews <= 0) coverup.enabled = false;
    }

    public void ResetAll()
    {
        foreach (var screw in screws)
        {
            coverup.enabled = true;
            _activeScrews = 4;
            screw.SetActive(true);
        }
    }
}
