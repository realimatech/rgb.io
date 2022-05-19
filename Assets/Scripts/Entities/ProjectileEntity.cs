using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace realima.rgb
{
    public class ProjectileEntity : MonoBehaviour, ISpawnable
    {
        [SerializeField]
        private Rigidbody _body;
        [SerializeField]
        private LayerMask _layerMask = -1;
        [SerializeField]
        private int amount = 1;
        [SerializeField]
        private float _duration = 5;
        [SerializeField]
        private float _speed;
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                _body.velocity = transform.forward * _speed;
            }
        }

        private UnityEvent hitEvent;
        private UnityEvent burnoutEvent;

        private float enlapsedCost;
        public Action<GameObject> despawnEvent { get; set; }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
            gameObject.SetActive(true);
            enlapsedCost = 0;
        }

        private void FixedUpdate()
        {
            ProjectileMovement();
            HandleDuration();
        }

        private void ProjectileMovement()
        {
            var movement = transform.forward * _speed * Time.deltaTime;
            transform.Translate(movement);
        }

        private void HandleDuration()
        {
            enlapsedCost += Time.deltaTime;
            if (enlapsedCost > _duration)
            {
                burnoutEvent?.Invoke();
                despawnEvent?.Invoke(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != _layerMask.value)
            {
                IDamageable damageableObj = other.GetComponent<IDamageable>() ?? other.GetComponentInParent<IDamageable>();
                if(damageableObj != null && !damageableObj.IsPhasing)
                {
                    damageableObj.Damage(amount, gameObject);
                }
                hitEvent?.Invoke();
                despawnEvent?.Invoke(this.gameObject);
            }
        }
    }
}
