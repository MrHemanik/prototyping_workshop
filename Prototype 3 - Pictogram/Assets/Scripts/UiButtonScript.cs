using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonScript : MonoBehaviour
{
    public RectTransform shopPanel;


    public void OpenShop()
    {
        shopPanel.localPosition = new Vector3((Screen.width/2f),0,0);
    }

    public void CloseShop()
    {
        shopPanel.localPosition = new Vector3((Screen.width / 2f)+shopPanel.rect.width, 0, 0);
    }
}
