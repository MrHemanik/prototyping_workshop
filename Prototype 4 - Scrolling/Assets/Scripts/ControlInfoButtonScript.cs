using UnityEngine;

public class ControlInfoButtonScript : MonoBehaviour
{
    public GameObject controlInfo;

    public AudioSource audSou;
    
    public void ToggleControlInfo()
    {
        audSou.Play();
        controlInfo.SetActive(!controlInfo.activeSelf);
    }
}
