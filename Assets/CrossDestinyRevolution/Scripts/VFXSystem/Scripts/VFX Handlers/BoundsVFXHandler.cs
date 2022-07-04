using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VFXSystem
{
    public class BoundsVFXHandler : MonoBehaviour, IVFXHandler
    {
        [SerializeField]
        private Transform[] _TrackedTransforms;
        
        private MeshRenderer _MeshRenderer;
        private Material _Material;
        private bool _IsActive = false;

        public Transform[] trackedTransforms { get => _TrackedTransforms; set => _TrackedTransforms = value; }
        public bool isActive => _IsActive;


        private void Start()
        {
            _MeshRenderer = GetComponent<MeshRenderer>();

            _Material = _MeshRenderer.material;
        }

        private void Update() 
        {
            if(!isActive)
                return;

            _Material.SetVectorArray("_Positions", trackedTransforms.Select(t => new Vector4(t.position.x, t.position.y, t.position.z)).ToArray());
        }

        public void Activate()
        {
            _IsActive = true;
        }

        public void Deactivate()
        {
            _IsActive = false;

            _Material.SetVectorArray("_Positions", new Vector4[2]);
        }
    }
}