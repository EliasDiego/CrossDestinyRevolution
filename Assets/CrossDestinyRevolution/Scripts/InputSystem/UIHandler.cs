using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CDR.InputSystem
{
    [RequireComponent(typeof(Image))]
    public class UIHandler : MonoBehaviour
    {
        RectTransform _RectTransform;
        PlayerUIInput _PlayerUIInput;

        private void Awake()
        {
            _RectTransform = transform as RectTransform;
            
            _PlayerUIInput = GetComponent<PlayerUIInput>();
            
            _PlayerUIInput.onCurrentSelectableChange += OnCurrentSelectableChange;
        }

        private void OnCurrentSelectableChange(Selectable selectable)
        {
            RectTransform selectableRectTransform = selectable.transform as RectTransform;

            _RectTransform.position = selectableRectTransform.position; 
            _RectTransform.sizeDelta = selectableRectTransform.sizeDelta;
        }
    }
}