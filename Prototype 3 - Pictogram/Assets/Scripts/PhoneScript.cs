using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    public GameManager gm;


    // Update is called once per frame
    public void OnClick()
    {
        gm.AddHeartsPerClick();
    }
}
