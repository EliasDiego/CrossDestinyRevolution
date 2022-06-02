using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using CDR.TargetingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CDR.UISystem
{
    public class TargetHandlerUI : MonoBehaviour, ITargetHandlerUI
    {
        [SerializeField] Camera _camera;
        [SerializeField] Image lockImage;

        IActiveCharacter _activeCharacter;
        ITargetData _targetData;

        bool _isShown;
        public bool isShown => _isShown;

        public void Hide()
        {
            lockImage.enabled = false;
            _isShown = false;
        }

        public void Show()
        {
            lockImage.enabled = true;
            _isShown = true;
        }

        public void SetTarget(IActiveCharacter target)
        {
            _activeCharacter = target;
        }

        public void SetTargetData(ITargetData targetData)
        {
            _targetData = targetData;
        }

        private void LateUpdate()
        {
            Vector2 pos = _camera.WorldToScreenPoint(_activeCharacter.position);
            lockImage.rectTransform.position = pos;
        }
    }
}