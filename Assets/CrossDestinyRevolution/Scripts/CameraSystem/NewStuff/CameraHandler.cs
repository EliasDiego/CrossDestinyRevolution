using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Cinemachine;

using CDR.MechSystem;
using CDR.TargetingSystem;

namespace CDR.CameraSystem.NewStuff
{
    public class CameraHandler : MonoBehaviour
    {
        [field: SerializeField]
        public ActiveCharacter activeCharacter { get; set;}
        [field: SerializeField]
        public CinemachineVirtualCamera virtualCamera { get; private set; }
        [SerializeField]
        private float maxY, maxZ;
        [SerializeField]
        private float defaultY, defaultZ;
        [SerializeField]
        private float _OwnerWeight, _OwnerRadius;
        [SerializeField]
        private float _TargetWeight, _TargetRadius;

        private CinemachineTargetGroup _TargetGroup;

        private Camera _Camera;

        private IActiveCharacter _Target;
        private float currentY = 0f;
        private float currentZ = 0f;

        private bool _IsActive = false;

        public new Camera camera => _Camera;
        public bool isActive => _IsActive;

        private void Awake() 
        {
            virtualCamera = Instantiate(virtualCamera.gameObject)?.GetComponent<CinemachineVirtualCamera>();

            _TargetGroup = new GameObject("Target Group")?.AddComponent<CinemachineTargetGroup>();

            _Camera = new GameObject("Camera")?.AddComponent<Camera>();

            _Camera.gameObject.AddComponent<CinemachineBrain>();

            ToggleGameObjects(false);
        }

        private void Update() 
        {
            if(!isActive || !activeCharacter.targetHandler.isActive)
                return;

            SetPos();    
        }

        private void ToggleGameObjects(bool value)
        {
            _Camera.gameObject.SetActive(value);
            _TargetGroup.gameObject.SetActive(value);
            virtualCamera.gameObject.SetActive(value);
        }

        private void SetTarget(IActiveCharacter target)
        {
            _Target = target;

            if(_TargetGroup.m_Targets.Length > 1)
                _TargetGroup.m_Targets[1].target = target.controller.transform;

            else
                _TargetGroup.AddMember(target.controller.transform, _TargetWeight, _TargetRadius);
        }

        private void OnSwitchTarget(ITargetData targetData)
        {
            SetTarget(targetData.activeCharacter);
        }
        
        private void SetPos()
        {
            var diffY = YDiffToTarget();
            var difference = Mathf.Floor(diffY);
            var interpolation = Mathf.Abs(diffY) / 50f;

            // Character is below target
            if (difference < -1f)
            {     
                currentY = Mathf.Lerp(defaultY, 0f, interpolation);
                currentZ = defaultZ;
            }
            // Character is above target
            else if (difference > 1f)
            {               
                currentY = Mathf.Lerp(defaultY, maxY, interpolation);               
                currentZ = Mathf.Lerp(defaultZ, maxZ, interpolation) + 0.35f;
            }
            else
            {
                currentY = defaultY;
                currentZ = defaultZ;
            }
            transform.localPosition = new Vector3(0, currentY, currentZ);
            // transform.position = Vector3.Lerp(transform.position, _ActiveCharacter.position + _ActiveCharacter.rotation * new Vector3(0, currentY, currentZ), .5f);
        }

        private float YDiffToTarget()
        {
            return activeCharacter.position.y - _Target.position.y;
        }

        public void Enable()
        {
            if(!activeCharacter)
                return;
            
            activeCharacter.targetHandler.onSwitchTarget += OnSwitchTarget;

            if(activeCharacter.targetHandler.isActive)
                SetTarget(activeCharacter.targetHandler.GetCurrentTarget()?.activeCharacter);

            _TargetGroup.AddMember(activeCharacter.transform, _OwnerWeight, _OwnerRadius);

            virtualCamera.Follow = transform;
            virtualCamera.LookAt = _TargetGroup.Transform;

            _IsActive = true;

            ToggleGameObjects(true);
        }

        public void Disable()
        {
            if(activeCharacter)
                activeCharacter.targetHandler.onSwitchTarget -= OnSwitchTarget;

            _TargetGroup.m_Targets = null;

            _Target = null;

            _IsActive = false;

            ToggleGameObjects(false);
        }
    }
}