using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour
{
    public Transform switchVisual;
    private Image switchVisualImage;
    private Color inactive = Color.red;
    private Color active = Color.green;
    public bool state = false;

    private void Start()
    {
        switchVisualImage = switchVisual.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        var localPosition = switchVisual.localPosition;
        localPosition = Vector3.Lerp(localPosition, new Vector3(0, (state? -50:50), 0), 0.2f);
        switchVisual.localPosition = localPosition;
        switchVisualImage.color = Color.Lerp(active,inactive,  (localPosition.y+50)/100);
    }

    public void ToggleState()
    {
        state = !state;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
