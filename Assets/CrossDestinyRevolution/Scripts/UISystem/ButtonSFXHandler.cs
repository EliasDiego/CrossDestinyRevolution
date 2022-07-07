using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using CDR.AudioSystem;

namespace CDR.UISystem
{
    public class ButtonSFXHandler : MonoBehaviour, ISelectHandler, IPointerEnterHandler, ISubmitHandler
    {
        [SerializeField]
        AudioSource _AudioSource;
        [SerializeField]
        AudioClipPreset _OnSubmitSFX;
        [SerializeField]
        AudioClipPreset _OnSelectSFX;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _OnSelectSFX?.PlayOneShot(_AudioSource);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _OnSelectSFX?.PlayOneShot(_AudioSource);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _OnSubmitSFX?.PlayOneShot(_AudioSource);
        }
    }
}