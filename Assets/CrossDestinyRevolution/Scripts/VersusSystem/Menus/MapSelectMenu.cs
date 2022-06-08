using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.InputSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class MapSelectMenu : VersusMenu, IMapSelectMenu, IMenuCancelHandler
    {
        [SerializeField]
        VersusSettingsMenu _VersusSettingsMenu;

        public void OnCancel()
        {
            Back();
        }

        public void PickMap(IMapData mapData)
        {
            versusData.mapData = mapData;

            SwitchTo(_VersusSettingsMenu);
        }
    }
}