using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class MapSelectMenu : VersusMenu, IMapSelectMenu
    {
        [SerializeField]
        VersusSettingsMenu _VersusSettingsMenu;

        public void PickMap(IPlayerInput playerInput, IMapData mapData)
        {
            versusData.mapData = mapData;

            SwitchTo(_VersusSettingsMenu);
        }

        public override void Show()
        {
            base.Show();

            player1Input.EnableInput();
            player2Input.EnableInput();
        }
    }
}