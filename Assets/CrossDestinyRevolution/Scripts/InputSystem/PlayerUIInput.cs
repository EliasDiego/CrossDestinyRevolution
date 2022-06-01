using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace CDR.InputSystem
{
    public class PlayerUIInput : PlayerInput
    {
        private Selectable _CurrentSelectable;
        private Vector2 _MousePosition;

        private bool _IsClicked = false;

        private RectTransform _RectTransform;

        public event Action<Selectable> onSubmit;
        public event Action<Selectable> onCancel;
        public event Action<Selectable> onCurrentSelectableChange;
        public event Action onEnableInput;
        public event Action onDisableInput;

        public Selectable currentSelectable { get => _CurrentSelectable; set => SetCurrentSelectable(value); }

        private void Awake() 
        {
            _RectTransform = (RectTransform)transform;
        }

        private void SetCurrentSelectable(Selectable selectable)
        {
            if(!selectable)
                return;

            _CurrentSelectable = selectable;

            onCurrentSelectableChange?.Invoke(_CurrentSelectable);
        }

        private bool IsInsideRect(RectTransform rectTransform, Vector2 point)
        {
            Rect rect = new Rect(rectTransform.position - (Vector3)rectTransform.sizeDelta / 2, rectTransform.sizeDelta);
            
            return rect.Contains(point);
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
            {
                currentSelectable = Selectable.allSelectablesArray.FirstOrDefault();

                return;
            }

            Selectable nextSelectable = currentSelectable.FindSelectable(context.ReadValue<Vector2>());

            if(nextSelectable)
                currentSelectable = nextSelectable;
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;

            onSubmit?.Invoke(currentSelectable);

            if(currentSelectable is ISubmitHandler s)
                s.OnSubmit(null);
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;

            onCancel?.Invoke(currentSelectable);

            if(currentSelectable is ICancelHandler c)
                c.OnCancel(null);
        }

        private void OnPoint(InputAction.CallbackContext context)
        {
            _MousePosition = context.ReadValue<Vector2>();

            Selectable selectable = Selectable.allSelectablesArray.FirstOrDefault(s => IsInsideRect(s.transform as RectTransform, _MousePosition));

            if(currentSelectable && currentSelectable != selectable)
                currentSelectable.OnPointerExit(null); 

            if(selectable)
            {
                currentSelectable = selectable;
                currentSelectable.OnPointerEnter(null);
            }
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;

            _IsClicked = !_IsClicked;

            if(!_IsClicked && currentSelectable is ISubmitHandler s)
            {
                s.OnSubmit(null);

                onSubmit?.Invoke(currentSelectable);
            }
        }

        public override void SetupInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            base.SetupInput(inputActionMap, devices);

            inputActions["Point"].performed += OnPoint;
            inputActions["Navigate"].started += OnNavigate;
            inputActions["Navigate"].performed += OnNavigate;
            inputActions["Submit"].started += OnSubmit;
            inputActions["Cancel"].started += OnCancel;
            inputActions["Click"].performed += OnClick;
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