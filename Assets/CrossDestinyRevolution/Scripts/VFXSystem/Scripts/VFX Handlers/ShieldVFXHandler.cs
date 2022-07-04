using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CDR.VFXSystem
{
    public class ShieldVFXHandler : MonoBehaviour, IVFXHandler
    {
        [Header("On Activate")]
        [SerializeField, GradientUsage(true)]
        private Gradient _ShieldGradient;
        [SerializeField]
        private AnimationCurve _RadiusCurve;
        [SerializeField]
        private float _Radius;
        [SerializeField]
        private float _ActivateTime;
        [SerializeField]
        private float _DeactivateTime;

        private MeshRenderer _MeshRenderer;

        private bool _IsActive = false;

        private Material _ShieldMaterial;

        private Coroutine _Coroutine;

        private float _RadiusCurveOffset;

        public bool isActive => _IsActive;

        private void Awake() 
        {
            _MeshRenderer = GetComponent<MeshRenderer>();
            
            _RadiusCurveOffset = 1 - _RadiusCurve.GetArea(0.001f);

            _ShieldMaterial = _MeshRenderer.material;

            _ShieldMaterial.color = Color.clear;
        }

        private void Ease(float easeValue)
        {
            _MeshRenderer.transform.localScale = Vector3.one * (_RadiusCurve.Evaluate(easeValue) + _RadiusCurveOffset) * _Radius;

            _ShieldMaterial.color = _ShieldGradient.Evaluate(easeValue);
        }

        public void Activate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _IsActive = true;

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseIn(Ease, () => Time.deltaTime, null, _ActivateTime));
        }

        public void Deactivate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

             _IsActive = false;

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseOut(Ease, () => Time.deltaTime, null, _DeactivateTime));
        }
    }
}