using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Pitch = Animator.StringToHash("pitch");
    public Sprite animal_open;
    public Sprite animal_close;
    private Image _image;
    

    void Start()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }
    
    public void SetPitch(float pitch, bool mouthState)
    {
        _animator.SetFloat(Pitch, pitch);
        if(mouthState)
            _image.sprite = animal_open;
        else
            _image.sprite = animal_close;
    }
}