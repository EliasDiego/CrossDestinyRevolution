using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MapButton : MonoBehaviour, ISubmitHandler, IPointerDownHandler
    {
        [SerializeField]
        MapSelectMenu _MapSelectMenu;
        [SerializeField]
        MapData _MapData;

        public void OnPointerDown(PointerEventData eventData)
        {
            _MapSelectMenu.PickMap(_MapData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _MapSelectMenu.PickMap(_MapData);
        }
    }
}