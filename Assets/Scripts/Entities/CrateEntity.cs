using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace realima.rgb
{
    public class CrateEntity : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _healthMax = 1;
        private int _health;

        [SerializeField]
        private UnityEvent destroyEvent;

        public bool IsPhasing => false;

        private void Awake()
        {
            _health = _healthMax;
        }

        public void Damage(int amount, GameObject gameObject = null)
        {
            _health -= amount;
            if(_health <= 0)
            {
                destroyEvent?.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}
