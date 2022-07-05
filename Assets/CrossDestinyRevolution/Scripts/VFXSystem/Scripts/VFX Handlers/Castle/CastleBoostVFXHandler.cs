using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class CastleBoostVFXHandler : BoostVFXHandler
    {
        [SerializeField]
        private Gradient _EmissionColorGradient;
        [SerializeField]
        private AnimationCurve _EmissionIntensityCurve;
        [SerializeField]
        private float _EmissionIntensity;
        [SerializeField]
        private AnimationCurve _HeightCurve;
        [SerializeField]
        private float _Height;
        [SerializeField]
        private float _Time;

        private Coroutine _Coroutine;

        private Material[] _Materials;

        private Vector3 _StartScale;
        
        private bool _IsActive = false;

        public override bool isActive => _IsActive;

        private void Awake() 
        {
            _Materials = GetComponentsInChildren<MeshRenderer>()?.Select(m => m.material)?.ToArray();

            foreach(Material material in _Materials)
            {
                material.SetColor("_EmissionColor", _EmissionColorGradient.Evaluate(0));
                material.SetFloat("_EmissionIntensity", _EmissionIntensityCurve.Evaluate(0) * _EmissionIntensity);
            }

            _StartScale = transform.lossyScale;
        }

        private void EaseEvent(float easeValue)
        {
            Vector3 scale = transform.localScale;

            foreach(Material material in _Materials)
            {
                material.SetColor("_EmissionColor", _EmissionColorGradient.Evaluate(easeValue));
                material.SetFloat("_EmissionIntensity", _EmissionIntensityCurve.Evaluate(easeValue) * _EmissionIntensity);
            }

            transform.localScale = _StartScale + Vector3.up * _HeightCurve.Evaluate(easeValue) * _Height;
        }

        public override void Activate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseIn(EaseEvent, () => Time.deltaTime, null, _Time));

            _IsActive = true;
        }

        public override void Deactivate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);
                
            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseOut(EaseEvent, () => Time.deltaTime, null, _Time));

            _IsActive = false;
        }
    }
}