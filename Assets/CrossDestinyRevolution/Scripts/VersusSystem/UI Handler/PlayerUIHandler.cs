using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerUIHandler : MonoBehaviour
    {
        RectTransform _RectTransform;
        PlayerUIInput _PlayerInput;
        SelectableEventHandler _SelectableEventHandler;
        Image _Image;

        void Awake()
        {
            _RectTransform = transform as RectTransform;

            _Image = GetComponent<Image>();

            _PlayerInput = GetComponent<PlayerUIInput>();

            _SelectableEventHandler = GetComponent<SelectableEventHandler>();

            _SelectableEventHandler.onCurrentSelectableChange += OnCurrentSelectableChange;
            _PlayerInput.onEnableInput += OnEnableInput;
            _PlayerInput.onDisableInput += OnDisableInput;
        }

        private void OnEnableInput(IInput input)
        {
            _Image.enabled = true;
            
            _RectTransform.position = Vector2.zero;
            _RectTransform.sizeDelta = Vector2.zero;
        }

        private void OnDisableInput(IInput input)
        {
            _Image.enabled = false;
        }

        private void OnCurrentSelectableChange(Selectable selectable)
        {
            RectTransform rectTransform = selectable.transform as RectTransform;

            _RectTransform.position = rectTransform.position;
            _RectTransform.sizeDelta = rectTransform.sizeDelta;
        }
    }
}