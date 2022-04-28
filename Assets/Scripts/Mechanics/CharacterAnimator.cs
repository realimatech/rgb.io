using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.rgb
{
    public class CharacterAnimator : MonoBehaviour
    {
        public float senseTimeInSecs = 1; 
        public float senseRandomnessRange = 1;

        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private CharacterMotion _motionComponent;
        [SerializeField]
        private CharacterAim _aimComponent;

        private int _lateHealth;
        private float _lateSpeedMag;
        private float _enlapsedIdleTime; //possibly go to motion component
        private float _nextSenseTime;

        float RandomSenseDelay() => senseTimeInSecs + Random.Range(-senseRandomnessRange / 2, senseRandomnessRange / 2);

        private void Awake()
        {
            _anim.SetInteger("Health", 1);
            _motionComponent.SpeedUpdateEvent += OnSpeedUpdate;
            _aimComponent.AttackEvent += OnAttack;
        }

        private void LateUpdate()
        {
            if (_anim == null) return;
            HandleSenseBehaviour();
        }

        private void HandleSenseBehaviour()
        {
            _enlapsedIdleTime = _lateSpeedMag > 0 ? 0 : _enlapsedIdleTime + Time.deltaTime;

            if (_enlapsedIdleTime > 0 && _nextSenseTime <= _enlapsedIdleTime)
            {
                _nextSenseTime += RandomSenseDelay();
            }
        }

        public void OnSpeedUpdate(Vector3 speed)
        {
            _anim.SetFloat("SpeedX", speed.x);
            _anim.SetFloat("SpeedY", speed.z);
            _lateSpeedMag = speed.magnitude;
            _anim.SetFloat("SpeedMag", _lateSpeedMag);
        }

        public void OnAttack()
        {
            _anim.SetTrigger("Attack");
            StartCoroutine(DelayedResetTrigger("Attack"));
        }

        private IEnumerator DelayedResetTrigger(string triggerName)
        {
            yield return new WaitForEndOfFrame();
            _anim.ResetTrigger("Attack");
            yield break;
        }

        public void OnHealthUpdate(int health, bool damage = true, bool stun = false)
        {
            if (health <= 0) _anim.SetTrigger("Die");
            else
            {
                if (health < _lateHealth && damage) _anim.SetTrigger("Hit");
                _anim.SetBool("Stun", stun);
            }
        }

        public void OnDieAnimationEnd()
        {

        }
    }
}
