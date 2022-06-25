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

        public event Action<InputAction.CallbackContext> onSubmit
        {
            remove
            {
                if(GetInputAction("Submit", out InputAction inputAction))
                    inputAction.started -= value;
            }
            add 
            {
                if(GetInputAction("Submit", out InputAction inputAction))
                    inputAction.started += value;
            }
        }

        public event Action<InputAction.CallbackContext> onCancel
        {
            remove
            {
                if(GetInputAction("Cancel", out InputAction inputAction))
                    inputAction.started -= value;
            }
            add 
            {
                if(GetInputAction("Cancel", out InputAction inputAction))
                    inputAction.started += value;
            }
        }

        public event Action<InputAction.CallbackContext> onMove
        {
            remove
            {
                if(GetInputAction("Move", out InputAction inputAction))
                {
                    inputAction.started -= value;
                    inputAction.performed -= value;
                }
            }
            add 
            {
                if(GetInputAction("Move", out InputAction inputAction))
                {
                    inputAction.started += value;
                    inputAction.performed += value;
                }
            }
        }

        public event Action<InputAction.CallbackContext> onPoint
        {
            remove
            {
                if(GetInputAction("Point", out InputAction inputAction))
                    inputAction.performed -= value;
            }
            add 
            {
                if(GetInputAction("Point", out InputAction inputAction))
                    inputAction.performed += value;
            }
        }

        public event Action<InputAction.CallbackContext> onClick
        {
            remove
            {
                if(GetInputAction("Click", out InputAction inputAction))
                    inputAction.performed -= value;
            }
            add 
            {
                if(GetInputAction("Click", out InputAction inputAction))
                    inputAction.performed += value;
            }
        }

        public event Action<InputAction.CallbackContext> onStart
        {
            remove
            {
                if(GetInputAction("Start", out InputAction inputAction))
                    inputAction.started -= value;
            }
            add 
            {
                if(GetInputAction("Start", out InputAction inputAction))
                    inputAction.started += value;
            }
        }

        public event Action<InputAction.CallbackContext> onPause
        {
            remove
            {
                if(GetInputAction("Pause", out InputAction inputAction))
                    inputAction.started -= value;
            }
            add 
            {
                if(GetInputAction("Pause", out InputAction inputAction))
                    inputAction.started += value;
            }
        }
    }
}