using System.Collections;
using UnityEngine;
using CDR.ActionSystem;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    [RequireComponent(typeof(BoostValue))]
    public class Boost : Action
    {
        [SerializeField]
        private BoostInfo info;

        public System.Action OnBoostUse;
        public System.Action OnBoostEnd;

        private BoostValue value;

        private Rigidbody rb;

        private void Awake()
        {
            value = GetComponent<BoostValue>();           
        }

        private void Start()
        {
            StartCoroutine(value.Regenerate(info.regenRate));
            Use();
        }

        // TODO: replace to use input system
        //  +  : replace to use in action system
        private void Update()
        {         
            if(isActive)
            {
                // Vertical boost
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    UseVerticalBoost(1f);
                }
                // Horizontal boost
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UseHorizontalBoost(rb.transform.forward);
                }
            }
        }

        public void SetRigidbody(Rigidbody rigid)
        {
            rb = rigid;
        }

        // Use this method for horizontal boost with input system.
        public void UseHorizontalBoost(Vector3 direction)
        {
            if(value.CanUse(info.hDashConsRate))
            {
                StartCoroutine(HorizontalBoost(direction));
            }
        }

        // Use this method for vertical boost with input system.
        public void UseVerticalBoost(float direction)
        {
            if (value.CanUse(info.vDashConsRate))
            {
                StartCoroutine(VerticalBoost(direction));
            }
        }

        public override void Use()
        {
            base.Use();
        }

        public override void End()
        {
            base.End();
        }

        private IEnumerator HorizontalBoost(Vector3 direction)
        {
            value.Consume(info.vDashConsRate);
            value.SetIsRegening(false);
            OnBoostUse?.Invoke();
            rb.AddForce(direction * info.horzDashDistance * 2f, ForceMode.VelocityChange);
            yield return new WaitForSeconds(info.vDashUseTime);
            OnBoostEnd?.Invoke();
            value.SetIsRegening(false);
        }

        private IEnumerator VerticalBoost(float direction)
        {
            value.Consume(info.hDashConsRate);
            value.SetIsRegening(false);
            OnBoostUse?.Invoke();
            rb.AddForce(new Vector3(0f, direction * info.horzDashDistance, 0f), ForceMode.VelocityChange);
            yield return new WaitForSeconds(info.hDashUseTime);
            OnBoostEnd?.Invoke();
            value.SetIsRegening(false);
        }
    }
}