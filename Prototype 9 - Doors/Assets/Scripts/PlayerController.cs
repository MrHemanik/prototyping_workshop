using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Using new input system, 3D movement with jump and OnLook for mouse
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private bool _jumpInput;
    private bool _isGrounded = true;
    private Transform _cameraTransform;
    public bool[] _isHoldingKeys = new bool[3];
    public MessagePopupScript mps;
    public bool _hasAxe;
    public GameObject endScreenObjects;
    public GameObject mainCanvas;
    public AudioManager am;
    public GameObject axe;
    private Animator _axeAnimator;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = transform.Find("UnityCamera");
        _axeAnimator = axe.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        //use time.fixedDeltaTime
        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y);
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;
        move.Normalize();
        _rb.MovePosition(transform.position + move * (speed * Time.fixedDeltaTime));
        if (_jumpInput && _isGrounded)
        {
            _rb.AddForce(Vector3.up * (jumpForce * Time.fixedDeltaTime), ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.ReadValueAsButton();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall")) return;
        _isGrounded = true;
    }
    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        //Do a raycast in direction of where the camera is looking, if it hits a key, pick it up
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, 10f))
        {
            switch (hit.collider.tag)
            {
                case "Key":
                    _isHoldingKeys[hit.collider.GetComponent<KeyData>().keyNumber] = true;
                    Destroy(hit.collider.gameObject);
                    mps.ShowMessage("You picked up a key!");
                    am.Play("pickupKey");
                    break;
                case "OpenableDoor":
                {
                    var keyNum = hit.collider.GetComponent<KeyData>().keyNumber;
                    if (_isHoldingKeys[keyNum])
                    {
                        _isHoldingKeys[keyNum] = false;
                        StartCoroutine(UnlockAndOpenDoor(hit.collider));
                        
                    }
                    else
                    {
                        mps.ShowMessage("You don't seem to have the right key to open the lock...");
                        am.Play("lockedDoor");
                    }

                    break;
                }
                case "UnopenableDoor":
                    mps.ShowMessage("This door doesn't seem to budge...");
                    am.Play("lockedDoor");
                    break;
                case "Axe":
                    mps.ShowMessage("You picked up the axe!");
                    Destroy(hit.collider.gameObject);
                    am.Play("pickupAxe");
                    _hasAxe = true;
                    axe.SetActive(true);
                    break;
                case "AxeBreakable":
                    if (_hasAxe)
                    {
                        StartCoroutine(BreakWood(hit.collider));
                    }
                    else
                    {
                        mps.ShowMessage("If you had an axe, you could break this...");
                        am.Play("noAxe");
                    }
                    break;
                case "ExitLadder":
                    endScreenObjects.SetActive(true);
                    mainCanvas.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    am.Play("win");
                    break;
            }
        }
    }
    private IEnumerator UnlockAndOpenDoor(Collider door)
    {
        mps.ShowMessage("You opened the door with the key!");
        am.Play("unlockingDoor");
        door.tag = "OpenDoor";
        door.transform.GetComponentInChildren<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(am.GetSoundLength("unlockingDoor"));
        am.Play("openingDoor");
    }

    private IEnumerator BreakWood(Collider wood)
    {
        _axeAnimator.SetTrigger("Attack");
        am.Play("woodBreaking");
        yield return new WaitForSeconds(0.4f);
        Destroy(wood.gameObject);
        mps.ShowMessage("You destroyed the barrier with your axe!");
    }
    
}
