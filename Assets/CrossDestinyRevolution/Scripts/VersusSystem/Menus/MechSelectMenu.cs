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
        [SerializeField]
        AIMechData[] _MechDatas;

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

            if(versusData.participantDatas.Count(p => p is PlayerParticipantData) > 1)
            {
                PlayerParticipantData[] datas = versusData.participantDatas.Cast<PlayerParticipantData>().ToArray();

                for(int i = 0; i < datas.Length; i++)
                {
                    playerInputs[i].AssignInput(datas[i].actionAsset.FindActionMap("UI"), datas[i].devices);
                    playerInputs[i].EnableInput();
                }
            }

            else
            {
                PlayerParticipantData playerData = versusData.participantDatas.First(p => p is PlayerParticipantData) as PlayerParticipantData;

                playerInputs[0].AssignInput(playerData.actionAsset.FindActionMap("UI"), playerData.devices);
                playerInputs[0].EnableInput();

                AIMechData aiData = _MechDatas[UnityEngine.Random.Range(0, _MechDatas.Length)];
                
                (versusData.participantDatas[1] as AIParticipantData).settings = aiData.settings;
                versusData.participantDatas[1].mechData = aiData;
            }
        }
    }
}