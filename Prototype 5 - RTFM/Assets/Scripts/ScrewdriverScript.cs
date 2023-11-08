using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverScript : MonoBehaviour
{
    public GameManager gm;
    public void PickupScrewdriver()
    {
        gm.hasScrewdriver = true;
        gameObject.SetActive(false);
    }
}
