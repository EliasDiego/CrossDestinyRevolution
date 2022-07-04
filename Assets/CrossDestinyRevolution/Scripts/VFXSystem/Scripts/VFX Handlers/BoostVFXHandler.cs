using System.Collections;
using System.Collections.Generic;


using UnityEngine;

using CDR;

namespace CDR.VFXSystem
{
    public class BoostVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        private AnimationCurve _HeightCurve;
        [SerializeField]
        private float _Height;
        [SerializeField]
        private float _Time;

        private Coroutine _Coroutine;
        
        private bool _IsActive = false;
        private float _HeightCurveOffset = 0;

        public bool isActive => _IsActive;

        private void Awake() 
        {
            _HeightCurveOffset = 1 - _HeightCurve.GetArea(0.001f);
        }

        private void EaseEvent(float easeValue)
        {
            Vector3 scale = transform.localScale;

            scale.y = (_HeightCurve.Evaluate(easeValue) + _HeightCurveOffset) * _Height;

            transform.localScale = scale;
        }

        public void Activate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseIn(EaseEvent, () => Time.deltaTime, null, _Time));

            _IsActive = true;
        }

        public void Deactivate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);
                
            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseOut(EaseEvent, () => Time.deltaTime, null, _Time));

            _IsActive = false;
        }
    }
}