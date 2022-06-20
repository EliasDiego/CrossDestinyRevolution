using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace CDR.InputSystem
{
    public class SelectableEventHandler : MonoBehaviour
    {
        private PlayerUIInput _PlayerUIInput;
        private Selectable _CurrentSelectable;

        private bool _IsClicked = false;
        private Vector2 _MousePosition;

        public Selectable currentSelectable { get => _CurrentSelectable; set => SetCurrentSelectable(value); }
        public event System.Action<Selectable> onCurrentSelectableChange;

        private void Awake()
        {
            _PlayerUIInput = GetComponent<PlayerUIInput>();

            _PlayerUIInput.onEnableInput += OnEnableInput;
            _PlayerUIInput.onDisableInput += OnDisableInput;
        }

        private void OnEnableInput()
        {
            _PlayerUIInput.onSubmit += OnSubmit;
            _PlayerUIInput.onCancel += OnCancel;
            _PlayerUIInput.onMove += OnMove;
            _PlayerUIInput.onPoint += OnPoint;
            _PlayerUIInput.onClick += OnClick;

        }

        private void OnDisableInput()
        {
            _PlayerUIInput.onSubmit -= OnSubmit;
            _PlayerUIInput.onCancel -= OnCancel;
            _PlayerUIInput.onMove -= OnMove;
            _PlayerUIInput.onPoint -= OnPoint;
            _PlayerUIInput.onClick -= OnClick;
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

        private void OnMove(InputAction.CallbackContext context)
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

            if(currentSelectable is ISubmitHandler s)
                s.OnSubmit(null);
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;

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

            if(_IsClicked)
                return;
                
            OnSubmit(context);
        }
    }
}