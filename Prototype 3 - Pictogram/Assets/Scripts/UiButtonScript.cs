using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonScript : MonoBehaviour
{
    public RectTransform shopPanel;
    public AudioSource asOpen;
    public AudioSource asClose;
    
    public void OpenShop()
    {
        shopPanel.localPosition = Vector3.zero;
        asOpen.Play();
    }

    public void CloseShop()
    {
        shopPanel.localPosition = new Vector3(shopPanel.rect.width, 0, 0);
        asClose.Play();
    }
}
