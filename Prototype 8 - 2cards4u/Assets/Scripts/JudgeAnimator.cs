using UnityEngine;
using UnityEngine.UI;

public class JudgeAnimator : MonoBehaviour
{
    public Sprite[] positive;
    public Sprite[] neutral;
    public Sprite[] negative;
    private Animator _animator;
    private Image _image;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }


    public void SetAnimation(int state)
    {
        Debug.Log("setAnimationOfJudge");
        _animator.SetTrigger("SpriteChange");
        if (state == 2)
        {
            _image.sprite = positive[Random.Range(0, positive.Length)];
        }
        else if (state == 1)
        {
            _image.sprite = neutral[Random.Range(0, neutral.Length)];
        }
        else if (state == 0)
        {
            _image.sprite = negative[Random.Range(0, negative.Length)];
        }
        
    }
}
