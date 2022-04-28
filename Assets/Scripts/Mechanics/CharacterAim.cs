using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace realima.rgb
{

    public class CharacterAim : MonoBehaviour
    {
        public LayerMask castMask;

        [SerializeField]
        private NavMeshAgent _agent;

        private Camera _cam;

        public Vector2 LastPoint { get; private set; }
        public Action AttackEvent { get; internal set; }

        private void Awake()
        {
            if (_agent != null)
                _agent.updateRotation = false;

            _cam = Camera.main;
        }

        private void Update()
        {
            if (_agent != null)
            {
                Ray screenRay = _cam.ScreenPointToRay(LastPoint);
                RaycastHit hit;
                if (Physics.Raycast(screenRay, out hit, Mathf.Infinity, castMask) && 
                    Vector3.Distance(hit.point, _agent.transform.position) > _agent.radius) //Fix aim glitch
                {
                    _agent.transform.LookAt(new Vector3(hit.point.x, 0, hit.point.z), Vector3.up);
                }
            }
        }

        public void AimAt(InputAction.CallbackContext callback)
        {
            LastPoint = callback.ReadValue<Vector2>();
        }

        public void Attack(InputAction.CallbackContext callback)
        {
            AttackEvent?.Invoke();
        }
    }
}
