using System.Linq;
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
            // if(player1Input == (PlayerUIInput)playerInput)
            // {
            //     versusData.player1Data.mechData = null;

            //     playerInput.EnableInput();
            // }
            
            // else if(player2Input == (PlayerUIInput)playerInput)
            // {
            //     versusData.player2Data.mechData = null;

            //     playerInput.EnableInput();
            // }

            // else if(versusData.participantDatas.Any(p => p.mechData != null))
            if(versusData.participantDatas.Any(p => p.mechData != null))
            {
                Debug.Log("Back");

                Back();
            }
        }

        public void PickMech(IPlayerInput playerInput, IMechData mechData)
        {
            // if(player1Input == (PlayerUIInput)playerInput)
            // {
            //     versusData.player1Data.mechData = mechData;

            //     playerInput.DisableInput();
            //     playerInput.EnableInput("Cancel");
            // }
            

            // else if(player2Input == (PlayerUIInput)playerInput)
            // {
            //     versusData.player2Data.mechData = mechData;

            //     playerInput.DisableInput();
            //     playerInput.EnableInput("Cancel");
            // }

            if(!versusData.participantDatas.Any(p => p.mechData == null))
                SwitchTo(_MapSelectMenu);
        }

        public override void Show()
        {
            base.Show();

            for(int i = 0; i < versusData.participantDatas.Length; i++)
                versusData.participantDatas[i].mechData = null;

            player1Input.EnableInput();
            player2Input.EnableInput();
        }
    }
}