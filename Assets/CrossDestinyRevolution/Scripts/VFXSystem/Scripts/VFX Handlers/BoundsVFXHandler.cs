using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MovementSystem;

namespace CDR.VFXSystem
{
    public class BoundsVFXHandler : MonoBehaviour, IVFXHandler
    {
        [field: SerializeField]
        public Transform[] trackedTransforms { get; set; }
        [SerializeField]
        private MeshRenderer _MeshRenderer;

        private Material _Material;
        private bool _IsActive = false;


        public bool isActive => _IsActive;

        private void Start()
        {
            _Material = _MeshRenderer.material;
        }

        private void Update() 
        {
            if(!isActive)
                return;

            if(trackedTransforms.Length <= 0)
                return;

            _Material.SetVectorArray("_Positions", trackedTransforms.Select(t => (Vector4)t.position).ToArray());
        }

        public void Activate()
        {
            _IsActive = true;

            _MeshRenderer.enabled = true;
        }

        public void Deactivate()
        {
            _IsActive = false;

            _MeshRenderer.enabled = false;

            _Material.SetVectorArray("_Positions", new Vector4[2]);
        }
    }
}