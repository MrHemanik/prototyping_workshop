using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsVisualizer : MonoBehaviour
{
    public NumberVisualizer hpc;
    public NumberVisualizer hpac;
    public NumberVisualizer hpaci;
    
    public void UpdateHPC(int hpcNum)
    {
        hpc.VisualizeHearts(hpcNum);
    }
    public void UpdateHPAC(int hpacNum)
    {
        hpac.VisualizeHearts(hpacNum);
    }
    public void UpdateHPACI(int hpaciNum)
    {
        hpaci.VisualizeHearts(hpaciNum);
    }

    public void VisualizeAutoStats()
    {
        hpac.transform.parent.gameObject.SetActive(true);
        hpaci.transform.parent.gameObject.SetActive(true);
    }
    
}
