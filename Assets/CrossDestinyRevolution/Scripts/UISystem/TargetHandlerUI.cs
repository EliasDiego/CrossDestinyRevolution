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
        [SerializeField] Image targetImage;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] float _heightOffset = 250f;
        [SerializeField] float minScale, maxScale;

        [SerializeField] Image targetHealthUI;

        public new Camera camera { get ; set; }

        ITargetData currentTarget;

        bool _isShown;
        public bool isShown => _isShown;

        public void Hide()
        {
            targetImage.enabled = false;
            targetHealthUI.enabled = false;
            _isShown = false;
        }

        public void Show()
        {
            targetImage.enabled = true;
            targetHealthUI.enabled = true;
            _isShown = true;
        }

        public void SetTarget(ITargetData targetData)
        {
            if(currentTarget != null)
                currentTarget.activeCharacter.health.OnModifyValue -= ChangeTargetHealthImage;

            currentTarget = targetData;

            currentTarget.activeCharacter.health.OnModifyValue += ChangeTargetHealthImage;
        }

        private void LateUpdate()
        {
            if(currentTarget != null)
            {
                Vector2 pos = camera.WorldToScreenPoint(currentTarget.activeCharacter.position);
                float distance = Vector3.Distance(transform.position, currentTarget.activeCharacter.position);

                // Check Camera Rect
                if(camera.rect.x == 0)
                {
                    rectTransform.localPosition = new Vector2(pos.x - (camera.pixelWidth * 0.5f), (pos.y + _heightOffset) - (camera.pixelHeight / 2));
                }
                else if(camera.rect.x == 0.5)
                {
                    rectTransform.localPosition = new Vector2(pos.x - (camera.pixelWidth * 1.5f), (pos.y + _heightOffset) - (camera.pixelHeight / 2));
                }

                // Scale Image based on Distance
                rectTransform.localScale = new Vector2(Mathf.Clamp((distance / 10), minScale, maxScale), 
                    Mathf.Clamp((distance / 10), minScale, maxScale));
            }
        }

        void ChangeTargetHealthImage(IValueRange valueRange)
        {
            targetHealthUI.fillAmount = valueRange.CurrentValue / valueRange.MaxValue;
        }
    }
}