using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float _speed = 0.5f;
    private float _targetZ = 1.5f;

    private float _lowestZ;
    private float _highestZ;
    private bool _firstScrollFinished;
    private bool _isMouseMovementOn = false;
    public GameObject controlInfo;
    public LevelGenerator lg;
    public LevelManager lm;

    private void Start()
    {
        (_lowestZ, _highestZ) = lg.GetRange();
        _lowestZ += 0.5f;
        _highestZ -= 0.5f;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(1.5f, 1.5f, Mathf.Lerp(transform.position.z, _targetZ, 0.2f));
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _targetZ += context.ReadValue<Vector2>().normalized.y * _speed;
            _targetZ = Math.Clamp(_targetZ, _lowestZ, _highestZ);
            return;
        }

        if (!_firstScrollFinished)
        {
            _firstScrollFinished = true;
            controlInfo.SetActive(false);
        }
    }

    public void OnChangeInputType(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("I");
        _isMouseMovementOn = !_isMouseMovementOn;
        Debug.Log(_isMouseMovementOn);
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (!_isMouseMovementOn) return;
        _targetZ = Math.Clamp(context.ReadValue<Vector2>().y/Screen.height*9, _lowestZ, _highestZ);
    }

    public void OnResetLevel(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        lm.ResetLevel();
    }
}