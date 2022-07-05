using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.VFXSystem
{
    public class MeleeAttackVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        private AnimationCurve _FadeCurve;
        [SerializeField]
        private AnimationCurve _SizeCurve;
        [SerializeField]
        private float _Size;
        [SerializeField]
        private float _Time;

        private float _SizeCurveOffset;
        private Material _Material;

        private Coroutine _Coroutine;

        private bool _IsActive = false;

        public bool isActive => _IsActive;

        private void Awake()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            _Material = meshRenderer.material;
                
            _Material.SetFloat("_FadeValue", 0);

            _SizeCurveOffset = 1 - _SizeCurve.GetArea(0.001f);
        }

        private void EaseEvent(float easeValue)
        {
            transform.localScale = Vector3.one * (_SizeCurve.Evaluate(easeValue) + _SizeCurveOffset) * _Size;
                
            _Material.SetFloat("_FadeValue", _FadeCurve.Evaluate(easeValue));
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