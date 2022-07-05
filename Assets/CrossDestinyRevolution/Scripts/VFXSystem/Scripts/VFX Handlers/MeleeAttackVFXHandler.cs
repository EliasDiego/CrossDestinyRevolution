using System.Linq;
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
        private float _Time;

        private float _SizeCurveOffset;
        private Material[] _Materials;

        private Coroutine _Coroutine;

        private bool _IsActive = false;

        public bool isActive => _IsActive;

        private void Awake()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            _Materials = GetComponentsInChildren<MeshRenderer>().Select(m => m.material).ToArray();

            foreach(Material m in _Materials)
                m.SetFloat("_FadeValue", 0);
        }

        private void EaseEvent(float easeValue)
        {
            foreach(Material m in _Materials)
                m.SetFloat("_FadeValue", _FadeCurve.Evaluate(easeValue));
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