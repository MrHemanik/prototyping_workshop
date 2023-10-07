using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class PlayerScript : MonoBehaviour
    {
        private Vector2 _movementVector;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (_movementVector.sqrMagnitude > 0.01)
            {
                rb.MovePosition(transform.position+new Vector3(_movementVector.x,_movementVector.y,0f) * (4f * Time.deltaTime));
            }
        }
        
        [UsedImplicitly]
        public void OnMovement(InputAction.CallbackContext context) //Beim Drücken der Move Tasten
        {
            _movementVector = context.ReadValue<Vector2>(); //Holt Vector2 Daten aus Movement
        }
    }
}
