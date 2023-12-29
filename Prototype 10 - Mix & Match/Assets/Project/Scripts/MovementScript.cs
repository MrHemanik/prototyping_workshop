using System;
using JetBrains.Annotations;
using TMPro;
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
        private Transform _ballVisualTransform;
        private (float min, float max) _ballSpeedRange = (min: 150f, max: 500f);
        private float _ballSpeed;
        private float _ballSpeedChargeRate = 150f;
        private float _planetGravity = 0.1f;
        private int _currentLevel = 0;
        private SpriteRenderer _directionSprite;
        private GameObject _speedbar;
        private Transform _speedbarChargeTransform;
        private SpriteRenderer _speedbarChargeSprite;
        private Color _speedbarChargeDefaultColor = new Color(0.145f, 0.875f, 1f);
        private Color _speedbarChargeFullColor = Color.red;
        private GameObject _deathsUIElement;
        private GameObject _endScreen;
        private TextMeshProUGUI _deathsText;
        private bool _isSpace = false;

        private int _deaths = 0;
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
            _ballVisualTransform = transform.Find("BallVisual").transform;

            _directionSprite = _ballDirectionTransform.GetChild(0).GetComponent<SpriteRenderer>();
            _speedbar = _ballDirectionTransform.GetChild(1).gameObject;
            _speedbarChargeTransform = _speedbar.transform.GetChild(0);
            _speedbarChargeSprite = _speedbarChargeTransform.GetChild(0).GetComponent<SpriteRenderer>();
            _deathsText = GameObject.Find("DeathsText").GetComponent<TextMeshProUGUI>();
            _deathsUIElement = GameObject.Find("DeathsUIElement");
            _endScreen = GameObject.Find("EndScreen");
            _endScreen.SetActive(false);
            _deathsUIElement.SetActive(false);
            LoadLevel(_currentLevel);
        }

        private void FixedUpdate()
        {
            switch (_state)
            {
                case BallState.ShootSpin:
                    _ballVisualTransform.rotation = _ballDirectionTransform.rotation;
                    break;
                case BallState.MovingStarted when _rb.velocity.magnitude > 0.1f:
                    _state = BallState.Moving;
                    break;
                case BallState.Moving when _rb.velocity.magnitude < 0.1f:
                    _rb.velocity = Vector2.zero;
                    _state = BallState.ShootSpin;
                    _directionSprite.enabled = true;
                    break;
                case BallState.Moving:
                    //Wanted to do this on collision, but the calculations didnt work out, this is easier to implement, though slower performance wise
                    _ballVisualTransform.rotation = Quaternion.Euler(0f, 0f,
                        Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg - 90);
                    break;
                case BallState.ShootSpeed:
                    _ballVisualTransform.rotation = _ballDirectionTransform.rotation;
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
                Debug.Log("Goal");
                _currentLevel++;
                LoadLevel(_currentLevel);
            }

            if (other.CompareTag("DamageWater"))
            {
                RestartLevel();
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Planet"))
            {
                var transform1 = other.transform;
                Vector2 direction = (transform1.position -transform.position);
                if(_rb.velocity != Vector2.zero)
                    _rb.velocity += direction.normalized * _planetGravity * transform1.localScale.x * Time.deltaTime * 100;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Planet"))
            {
                Debug.Log("Planet Collision");
                _rb.velocity = Vector2.zero;
            }
        }

        private void LoadLevel(int levelID)
        {
            _rb.velocity = Vector2.zero;
            _directionSprite.enabled = true;
            _speedbar.SetActive(false);
            _state = BallState.ShootSpin;
            transform.position = new Vector3((levelID < 4 ? 50f : 100f) * levelID, 0f, 0f);
            if(levelID < 4){
                _rb.drag = 1f;
                _rb.angularDrag = 0.1f;
                _rb.sharedMaterial.bounciness = 1f;
                _rb.sharedMaterial.friction = 0f;
            }
            if (levelID >= 4 && !_isSpace )
            {
                _rb.drag = 0f;
                _rb.angularDrag = 0f;
                _rb.sharedMaterial.bounciness = 0f;
                _rb.sharedMaterial.friction = 0f;
                Transform child = transform.GetChild(0);
                child.GetComponent<SpriteRenderer>().enabled = false;
                child.GetChild(0).gameObject.SetActive(true);
                _deathsUIElement.SetActive(true);
                _isSpace = true;
            }

            if (levelID >= 10)
                _endScreen.SetActive(true);
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
                    .normalized * 1f);
                _state = BallState.MovingStarted;
            }
        }
        
        [UsedImplicitly]
        public void OnRestartLevel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                RestartLevel();
            }
        }

        private void RestartLevel()
        {
            _deaths++;
            _deathsText.text = _deaths.ToString();
            LoadLevel(_currentLevel);
        }

        //Mouse movement
        [UsedImplicitly]
        public void OnMousePosition(InputAction.CallbackContext context)
        {
            _movementVector = context.ReadValue<Vector2>();
            var mousePosition = Camera.main.ScreenToWorldPoint(_movementVector);
            var direction = mousePosition - transform.position;
            _ballDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _ballDirectionTransform.rotation = Quaternion.Euler(0f, 0f, _ballDirection - 90);
        }
    }
}
