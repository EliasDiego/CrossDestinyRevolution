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
        [SerializeField] float _heightOffset = 250f;
        [SerializeField] float minScale, maxScale;

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

        public void SetTarget(ITargetData targetData)
        {
            currentTarget = targetData;
        }

        private void LateUpdate()
        {
            if(currentTarget != null)
            {
                Vector2 pos = _camera.WorldToScreenPoint(currentTarget.activeCharacter.position);
                float distance = Vector3.Distance(transform.position, currentTarget.activeCharacter.position);

                // Check Camera Rect
                if(_camera.rect.x == 0)
                {
                    targetImage.rectTransform.localPosition = new Vector2(pos.x - (_camera.pixelWidth * 0.5f), (pos.y + _heightOffset) - (_camera.pixelHeight / 2));
                }
                else if(_camera.rect.x == 0.5)
                {
                    targetImage.rectTransform.localPosition = new Vector2(pos.x - (_camera.pixelWidth * 1.5f), (pos.y + _heightOffset) - (_camera.pixelHeight / 2));
                }

                // Scale Image based on Distance
                targetImage.rectTransform.localScale = new Vector2(Mathf.Clamp((distance / 10), minScale, maxScale), 
                    Mathf.Clamp((distance / 10), minScale, maxScale));
            }
        }
    }
}