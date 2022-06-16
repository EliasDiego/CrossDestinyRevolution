using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

namespace CDR.InputSystem
{
    public class PlayerUIInput : PlayerInput
    {
        private Selectable _CurrentSelectable;
        private Vector2 _MousePosition;

        private bool _IsClicked = false;

        public event Action<InputAction.CallbackContext> onSubmit;
        public event Action<InputAction.CallbackContext> onCancel;
        public event Action<InputAction.CallbackContext> onMove;
        public event Action<InputAction.CallbackContext> onPoint;
        public event Action<InputAction.CallbackContext> onClick;
        public event Action onEnableInput;
        public event Action onDisableInput;

        public override void AssociateActionMap(InputActionMap inputActionMap)
        {
            base.AssociateActionMap(inputActionMap);

            inputActions["Point"].performed += onPoint;
            inputActions["Move"].started += onMove;
            inputActions["Move"].performed += onMove;
            inputActions["Submit"].started += onSubmit;
            inputActions["Cancel"].started += onCancel;
            inputActions["Click"].performed += onClick;
        }

        public override void EnableInput()
        {
            base.EnableInput();

            onEnableInput?.Invoke();
        }

        public override void DisableInput()
        {
            base.DisableInput();

            onDisableInput?.Invoke();
        }
    }
}