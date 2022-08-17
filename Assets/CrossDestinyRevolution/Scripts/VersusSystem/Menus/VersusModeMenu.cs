using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public class VersusModeMenu : AnimatedMenu
    {
        [SerializeField]
        private VersusData _VersusData;
        [SerializeField]
        private InputActionAsset _InputAsset;
        [SerializeField]
        private PlayerMechInputSettings _Settings;
        [SerializeField]
        private GameObject _BattleUIPrefab;
        [SerializeField]
        private GameObject _CameraPrefab;

        
        private IParticipantData SetPlayerData(string name, InputActionAsset actionAsset, params InputDevice[] devices)
        {
            return new PlayerParticipantData() 
            { 
                name = name,
                settings = _Settings,
                cameraPrefab = _CameraPrefab,
                actionAsset = actionAsset, 
                devices = devices, 
                battleUIPrefab = _BattleUIPrefab 
            }; 
        }

        public void SetAIMode()
        {
            _VersusData.participantDataList = new List<IParticipantData> { SetPlayerData("Player 1", _InputAsset, UnityEngine.InputSystem.InputSystem.devices.ToArray()), new AIParticipantData() };
        }
    }
}