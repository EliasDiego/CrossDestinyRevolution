using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class MechSelectMenu : MultipleUsersVersusMenu, IMechSelectMenu, IPlayerCancelHandler
    {
        [SerializeField]
        MapSelectMenu _MapSelectMenu;

        public void OnPlayerCancel(IPlayerInput playerInput)
        {
            if(player1Input == (PlayerUIInput)playerInput)
            {
                versusData.player1Data.mechData = null;

                playerInput.EnableInput();
            }
            
            else if(player2Input == (PlayerUIInput)playerInput)
            {
                versusData.player2Data.mechData = null;

                playerInput.EnableInput();
            }

            if(versusData.player1Data.mechData == null && versusData.player2Data.mechData == null)
            {
                Debug.Log("Back");

                Back();
            }
        }

        public void PickMech(IPlayerInput playerInput, IMechData mechData)
        {
            if(player1Input == (PlayerUIInput)playerInput)
            {
                versusData.player1Data.mechData = mechData;

                playerInput.DisableInput();
                playerInput.EnableInput("Cancel");
            }
            

            else if(player2Input == (PlayerUIInput)playerInput)
            {
                versusData.player2Data.mechData = mechData;

                playerInput.DisableInput();
                playerInput.EnableInput("Cancel");
            }

            if(versusData.player1Data.mechData != null && versusData.player2Data.mechData != null)
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