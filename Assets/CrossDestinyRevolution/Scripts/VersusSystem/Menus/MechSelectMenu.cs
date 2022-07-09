using System;
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
            int index = Array.FindIndex(playerInputs, p => p == (PlayerUIInput)playerInput);

            if(index == -1)
                return;

            if(!versusData.participantDatas.Any(p => p.mechData != null))
            {
                Debug.Log("Back");

                if(previousMenu != null)
                    Back();

                return;
            }

            versusData.participantDatas[index].mechData = null;

            playerInput.EnableInput();
        }

        public void PickMech(IPlayerInput playerInput, IMechData mechData)
        {
            int index = Array.FindIndex(playerInputs, p => p == (PlayerUIInput)playerInput);

            if(index == -1)
                return;

            versusData.participantDatas[index].mechData = mechData;

            playerInput.DisableInput();
            playerInput.EnableInput("Cancel");

            if(!versusData.participantDatas.Any(p => p.mechData == null))
                SwitchTo(_MapSelectMenu);
        }

        public override void Show()
        {
            base.Show();

            for(int i = 0; i < versusData.participantDatas.Length; i++)
                versusData.participantDatas[i].mechData = null;

            foreach(PlayerUIInput playerInput in playerInputs)
                playerInput.EnableInput();
        }
    }
}