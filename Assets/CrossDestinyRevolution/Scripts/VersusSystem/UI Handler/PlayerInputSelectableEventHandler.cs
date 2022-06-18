using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class PlayerInputSelectableEventHandler : MonoBehaviour
    {
        PlayerUIInput _PlayerInput;
        SelectableEventHandler _SelectableEventHandler;

        Selectable _CurrentSelectable;
        
        void Awake()
        {
            _PlayerInput = GetComponent<PlayerUIInput>();

            _SelectableEventHandler = GetComponent<SelectableEventHandler>();

            _PlayerInput.onEnableInput += OnEnableInput;
            _PlayerInput.onDisableInput += OnDisableInput;
        }

        private void OnEnableInput()
        {
            _PlayerInput.onSubmit += OnSubmit;
            _PlayerInput.onCancel += OnCancel;
        }

        private void OnDisableInput()
        {
            _PlayerInput.onSubmit -= OnSubmit;
            _PlayerInput.onCancel -= OnCancel;
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            IPlayerSubmitHandler handler = _SelectableEventHandler.currentSelectable?.GetComponent<IPlayerSubmitHandler>();

            if(handler == null)
                return;
                
            handler.OnPlayerSubmit(_PlayerInput);
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            IPlayerCancelHandler handler = _SelectableEventHandler.currentSelectable?.GetComponent<IPlayerCancelHandler>();

            if(handler == null)
                return;
                
            handler.OnPlayerCancel(_PlayerInput);
        }
    }
}