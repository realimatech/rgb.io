using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace realima.rgb
{
    public class CharacterMotion : MonoBehaviour
    {
        public float speedMultiplier;

        [SerializeField]
        private NavMeshAgent _agent;

        private Camera _cam;

        public Vector3 RelativeDirection { get; private set; }

        public Action<Vector3> SpeedUpdateEvent { get; internal set; }

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            if (_agent != null)
            {
                _agent.Move(RelativeDirection * speedMultiplier * Time.deltaTime);
            }
        }

        public void Move(InputAction.CallbackContext callback)
        {
            Vector2 inputDir = callback.ReadValue<Vector2>();
            RelativeDirection = _cam.transform.TransformDirection(inputDir);
            RelativeDirection = new Vector3(RelativeDirection.x, 0, RelativeDirection.y);
            RelativeDirection.Normalize();
            SpeedUpdateEvent?.Invoke(transform.InverseTransformDirection(RelativeDirection));
        }
    }
}
