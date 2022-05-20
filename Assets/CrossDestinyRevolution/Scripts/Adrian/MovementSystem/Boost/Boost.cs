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
                    if (value.CanUse(info.vDashConsRate))
                    {
                        StartCoroutine(UseBoost(BoostDirection.Vertical, info.vDashConsRate));
                    }
                }
                // Horizontal boost
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (value.CanUse(info.hDashConsRate))
                    {
                        StartCoroutine(UseBoost(BoostDirection.Horizontal, info.hDashConsRate, rb.transform.forward));
                    }
                }
            }
        }

        public void SetRigidbody(Rigidbody rigid)
        {
            rb = rigid;
        }

        public override void Use()
        {
            base.Use();
        }

        public override void End()
        {
            base.End();
        }

        /// <summary>
        /// Coroutine for using boost.
        /// </summary>
        /// <param name="direction">Horizontal or Vertical boost?</param>
        /// <param name="horzDirection">Direction to boost to.</param>
        /// <returns></returns>
        public IEnumerator UseBoost(BoostDirection direction, float consumeRate, Vector3 horzDirection = default)
        {
            value.Consume(consumeRate);
            value.SetIsRegening(false);
            OnBoostUse?.Invoke();
            Vector3 force = Vector3.zero;
            float wait = 0f;   

            switch (direction)
            {
                case BoostDirection.Vertical:
                    force = rb.transform.up.normalized * info.vertDashDistance;
                    wait = info.vDashUseTime;
                    break;
                case BoostDirection.Horizontal:
                    force = horzDirection.normalized * info.horzDashDistance;
                    wait = info.hDashUseTime;
                    break;
            }

            rb.AddForce(force, ForceMode.VelocityChange);
            yield return new WaitForSeconds(wait);
            OnBoostEnd?.Invoke();
            value.SetIsRegening(true);
        }
    }

    public enum BoostDirection
    {
        Vertical,
        Horizontal
    }
}