using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody _rb;
    private Vector3 _movement;
    private Animator _animator;
    private GameObject _spriteObject;
    
    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _spriteObject = transform.GetChild(0).gameObject;
    }
    public void Update()
    {
        _rb.velocity = _movement * speed;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _movement = Vector3.zero;
            _animator.SetTrigger("Idle");
        }
        else if (context.started) _animator.SetTrigger("Walk");
        else
        {
            Vector2 movement = context.ReadValue<Vector2>();
            _movement = new Vector3(movement.x, 0f, movement.y);
            if (movement.x != 0)
            {
                _spriteObject.transform.localScale = movement.x < 0 ? new Vector3(-2, 2, 2) : new Vector3(2, 2, 2);
            }

            
        }
    }
}
