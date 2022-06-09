using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusSettingsMenu : VersusMenu, IVersusSettingsMenu, IMenuCancelHandler
    {
        [SerializeField]
        MechSelectMenu _MechSelectMenu;

        private int _Rounds = 1;
        private int _RoundTime = 60;

        public void OnCancel()
        {
            Back();
        }

        public void SetRounds(int rounds)
        {
            _Rounds = rounds;
        }

        public void SetRoundTime(int roundTime)
        {
            _RoundTime = roundTime;
        }
        
        public void SetSettings()
        {
            versusData.settings = new VersusSettings(_Rounds, _RoundTime);

            SwitchTo(_MechSelectMenu);
        }
    }
}