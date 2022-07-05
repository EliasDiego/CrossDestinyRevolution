using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class RCRLaserVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        private AnimationCurve _FadeCurve;
        [SerializeField]
        private float _FadeTime;

        [SerializeField]
        private Transform _LaserHeadTransform;
        [SerializeField]
        private Transform _LaserBodyTransform;
        [SerializeField]
        private float _Length;
        
        private Material[] _Materials;

        private Coroutine _Coroutine;

        private bool _IsActive = false;

        public bool isActive => _IsActive;

        public float length { get => _Length; set => _Length = value; }

        private void Awake() 
        {
            _Materials = GetComponentsInChildren<MeshRenderer>()?.Select(m => m.material)?.ToArray();

            foreach(Material m in _Materials)
                m.SetFloat("_FadeValue", 0);    
        }

        private void Update() 
        {
            if(!isActive)
                return;

            _LaserHeadTransform.localPosition = Vector3.forward * length;

            _LaserBodyTransform.localPosition = new Vector3(0, 0, length / 2);
            _LaserBodyTransform.localScale = new Vector3(1, 1, length / 2);
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

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseIn(EaseEvent, () => Time.deltaTime, null, _FadeTime));

            _IsActive = true;
        }

        public void Deactivate()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseOut(EaseEvent, () => Time.deltaTime, null, _FadeTime));

            _IsActive = false;
        }
    }
}