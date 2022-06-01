using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MechSelectMenu : VersusMenu, IMechSelectMenu
    {
        [SerializeField]
        MapSelectMenu _MapSelectMenu;

        public void PickMech(IPlayerInput playerInput, IMechData mechData)
        {
            if(player1Input == (PlayerUIInput)playerInput)
                versusData.player1Data.mechData = mechData;

            else if(player2Input == (PlayerUIInput)playerInput)
                versusData.player2Data.mechData = mechData;

            SwitchTo(_MapSelectMenu);
        }

        public override void Show()
        {
            base.Show();

            versusData.player1Data.mechData = null;
            versusData.player2Data.mechData = null;

            player1Input.EnableInput();
            player2Input.EnableInput();
        }
    }
}