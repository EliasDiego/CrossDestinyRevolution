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

                /*Debug.Log("cam width: " + _camera.pixelWidth + "screen width: " + Screen.width);
                Debug.Log("cam height: " + _camera.pixelHeight + "screen height" + Screen.height);
                Debug.Log("Screen pos: " + pos);*/

                targetImage.rectTransform.localPosition = new Vector2(pos.x - (_camera.pixelWidth / 2), pos.y - (_camera.pixelHeight / 2));
            }
        }
    }
}