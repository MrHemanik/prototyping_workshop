using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    public GameManager gm;
    public AudioSource auds;
    public Animation anim;

    // Update is called once per frame
    public void OnClick()
    {
        gm.AddHeartsPerClick();
        auds.Play();
        anim.Stop();
        anim.Play();
    }
}
