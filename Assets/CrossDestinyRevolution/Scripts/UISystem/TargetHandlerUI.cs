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
        [SerializeField] Image targetImage;

        //IActiveCharacter _activeCharacter;
        ITargetData currentTarget;

        bool _isShown;
        public bool isShown => _isShown;

        public void Hide()
        {
            targetImage.enabled = false;
            _isShown = false;
        }

        public void Show()
        {
            targetImage.enabled = true;
            _isShown = true;
        }

        // public void SetTarget(IActiveCharacter target)
        // {
        //     _activeCharacter = target;
        // }

        public void SetTarget(ITargetData targetData)
        {
            currentTarget = targetData;
        }

        private void LateUpdate()
        {
            Vector2 pos = _camera.WorldToScreenPoint(currentTarget.activeCharacter.position);
            targetImage.rectTransform.position = pos;
        }
    }
}