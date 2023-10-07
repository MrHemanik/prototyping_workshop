using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class MovementScript : MonoBehaviour
    {
        private Vector2 _movementVector;
        private Rigidbody2D _rb;
        private float _ballDirection;
        private Transform _ballDirectionTransform;
        private (float min, float max) _ballSpeedRange = (min: 150f, max: 500f);
        public float _ballSpeed;
        private float _ballSpeedChargeRate = 150f;
        private float _spinSpeed = 150f;
        private int _currentLevel = 0;
        private SpriteRenderer _directionSprite;
        private GameObject _speedbar;
        private Transform _speedbarChargeTransform;
        private SpriteRenderer _speedbarChargeSprite;
        private Color _speedbarChargeDefaultColor = new Color(0.145f, 0.875f, 1f);
        private Color _speedbarChargeFullColor = Color.red;
        public enum BallState
        {
            ShootSpin,
            ShootSpeed,
            MovingStarted,
            Moving
        }

        public BallState _state = BallState.ShootSpin;

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _ballDirectionTransform = transform.Find("BallDirection").transform;
            _directionSprite = _ballDirectionTransform.GetChild(0).GetComponent<SpriteRenderer>();
            _speedbar = _ballDirectionTransform.GetChild(1).gameObject;
            _speedbarChargeTransform = _speedbar.transform.GetChild(0);
            _speedbarChargeSprite = _speedbarChargeTransform.GetChild(0).GetComponent<SpriteRenderer>();
            LoadLevel(_currentLevel);
        }

        private void FixedUpdate()
        {
            switch (_state)
            {
                case BallState.ShootSpin:
                
                    _ballDirection += Time.deltaTime * _spinSpeed;
                    if (_ballDirection > 360f) _ballDirection -= 360f;
                    _ballDirectionTransform.rotation = Quaternion.Euler(0f, 0f, -_ballDirection);
                    break;
                case BallState.MovingStarted when _rb.velocity.magnitude > 0.1f:
                    _state = BallState.Moving;
                    break;
                case BallState.Moving when _rb.velocity.magnitude < 0.1f:
                    _rb.velocity = Vector2.zero;
                    _state = BallState.ShootSpin;
                    _directionSprite.enabled = true;
                    break;
                case BallState.ShootSpeed:
                    if (_ballSpeed < _ballSpeedRange.max)
                    {
                        _ballSpeed = Mathf.Min(_ballSpeed + Time.deltaTime * _ballSpeedChargeRate, _ballSpeedRange.max);
                        _speedbarChargeTransform.localScale = new Vector3(1,
                            (_ballSpeed - _ballSpeedRange.min) / (_ballSpeedRange.max - _ballSpeedRange.min), 1);
                    }
                    else
                    {
                        _speedbarChargeSprite.color = _speedbarChargeFullColor;
                    }

                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Goal"))
            {
                _currentLevel++;
                LoadLevel(_currentLevel);
            }

            if (other.CompareTag("DamageWater"))
            {
                LoadLevel(_currentLevel);
            }
        }

        private void LoadLevel(int levelID)
        {
            _rb.velocity = Vector2.zero;
            transform.position = new Vector3(50f * levelID, 0f, 0f);
        }

        [UsedImplicitly]
        public void OnSpace(InputAction.CallbackContext context)
        {
            if (_state == BallState.ShootSpin && context.phase == InputActionPhase.Started)
            {
                _directionSprite.enabled = false;
                _ballSpeed = _ballSpeedRange.min;
                _state = BallState.ShootSpeed;
                _speedbarChargeTransform.localScale = new Vector3(1, 0, 1);
                _speedbarChargeSprite.color = _speedbarChargeDefaultColor;
                _speedbar.SetActive(true);
            }
            else if (_state == BallState.ShootSpeed && context.phase == InputActionPhase.Canceled)
            {
                _speedbar.SetActive(false);
                _rb.AddForce(_ballSpeed * (_ballDirectionTransform.GetChild(0).transform.position - transform.position)
                    .normalized);
                _state = BallState.MovingStarted;
                
            }
        }
    }
}