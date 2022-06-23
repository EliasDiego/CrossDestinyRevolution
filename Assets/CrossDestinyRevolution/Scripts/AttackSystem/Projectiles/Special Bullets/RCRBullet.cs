using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public class RCRBullet : Projectile
    {
        [Header("Unique properties")]
        [SerializeField]
        private GameObject pivot;
        [SerializeField]
        private GameObject beam;
        [SerializeField]
        private float maxLength;
        [SerializeField]
        private float timeToScale;
        [SerializeField]
        private float rotationSpeed;

        private bool enableRotation = false;

        private void Awake()
        {
            HitBox.onHitEnter += OnHitEnter;        
        }

        public override void OnEnable()
        {
            base.OnEnable();
            ScaleWithTime();
            //Debugger();
        }

        private void Debugger()
        {
            enableRotation = true;
            beam.transform.localScale = new Vector3(1f, 1f, maxLength);
        }

        private void OnDestroy()
        {       
            HitBox.onHitEnter -= OnHitEnter;            
        }

        public override void Update()
        {
            base.Update();
            Rotate();
        }

        //private void FixedUpdate()
        //{
        //    AddRbForce(new Vector3(0f, 0f, 0.01f));
        //}

        protected override void OnHitEnter(IHitEnterData hitData)
        {
            Debug.Log("ALSKNd");
        }
        public override void ResetObject()
        {
            enableRotation = false;
            pivot.transform.localScale = Vector3.zero;
            transform.localEulerAngles.Set(0f, 90f, 0f);
        }

        private void ScaleWithTime()
        {
            LeanTween.scale(beam, Vector3.one, 0.45f).setOnComplete(()=>
            {
                LeanTween.scaleZ(pivot, maxLength, timeToScale).setOnComplete(
                () =>
                {
                    enableRotation = true;
                });
            });
            
        }

        private void Rotate()
        {
            if (Mathf.Floor(270f - transform.localEulerAngles.y) != 0 && enableRotation)
            {
                transform.RotateAround(transform.position, transform.up, -rotationSpeed * Time.deltaTime);
            }

            //Rotate(Quaternion.Euler(0f, transform.localEulerAngles.y - (rotationSpeed * Time.deltaTime), 0f));
        }

    }
}
