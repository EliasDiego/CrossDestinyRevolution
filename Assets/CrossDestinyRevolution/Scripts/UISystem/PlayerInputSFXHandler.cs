using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using CDR.InputSystem;
using CDR.AudioSystem;

namespace CDR.UISystem
{
    
    public class PlayerInputSFXHandler : MonoBehaviour
    {
        [SerializeField]
        AudioSource _AudioSource;
        [SerializeField]
        AudioClipPreset _OnSubmitSFX;
        [SerializeField]
        AudioClipPreset _OnSelectSFX;
        [SerializeField]
        AudioClipPreset _OnCancelSFX;

        private Selectable _CurrentSelectable;

        private bool _IsClicked = false;
        private Vector2 _MousePosition;

        public Selectable currentSelectable { get; set; }
        public event System.Action<Selectable> onCurrentSelectableChange;

        private void Awake()
        {
            PlayerUIInput playerInput = GetComponent<PlayerUIInput>();

            playerInput.onEnableInput += OnEnableInput;
            playerInput.onDisableInput += OnDisableInput;
        }

        private void OnEnableInput(IInput input)
        {
            PlayerUIInput playerInput = input as PlayerUIInput;

            playerInput.onSubmit += OnSubmit;
            playerInput.onCancel += OnCancel;
            playerInput.onMove += OnMove;
            playerInput.onPoint += OnPoint;
            playerInput.onClick += OnClick;
        }

        private void OnDisableInput(IInput input)
        {
            PlayerUIInput playerInput = input as PlayerUIInput;
            
            playerInput.onSubmit -= OnSubmit;
            playerInput.onCancel -= OnCancel;
            playerInput.onMove -= OnMove;
            playerInput.onPoint -= OnPoint;
            playerInput.onClick -= OnClick;
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
            {
                currentSelectable = nextSelectable;

                _OnSelectSFX?.PlayOneShot(_AudioSource);
            }
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;
            
            _OnSubmitSFX?.PlayOneShot(_AudioSource);
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            if(!currentSelectable)
                return;

            _OnCancelSFX?.PlayOneShot(_AudioSource);
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