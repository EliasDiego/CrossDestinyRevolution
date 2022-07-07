using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField]
        Image[] _Images;

        RectTransform _RectTransform;
        PlayerUIInput _PlayerInput;
        SelectableEventHandler _SelectableEventHandler;

        void Awake()
        {
            _RectTransform = transform as RectTransform;

            _PlayerInput = GetComponent<PlayerUIInput>();

            _SelectableEventHandler = GetComponent<SelectableEventHandler>();

            _SelectableEventHandler.onCurrentSelectableChange += OnCurrentSelectableChange;
            _PlayerInput.onEnableInput += OnEnableInput;
            _PlayerInput.onDisableInput += OnDisableInput;
        }

        private void OnEnableInput(IInput input)
        {
            foreach(Image image in _Images)
                image.enabled = true;
            
            _RectTransform.position = Vector2.zero;
            _RectTransform.sizeDelta = Vector2.zero;
        }

        private void OnDisableInput(IInput input)
        {
            foreach(Image image in _Images)
                image.enabled = false;
        }

        private void OnCurrentSelectableChange(Selectable selectable)
        {
            RectTransform rectTransform = selectable.transform as RectTransform;

            _RectTransform.position = rectTransform.position;
            _RectTransform.sizeDelta = rectTransform.sizeDelta;
        }
    }
}