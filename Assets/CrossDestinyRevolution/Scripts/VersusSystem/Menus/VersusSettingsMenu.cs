using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusSettingsMenu : VersusMenu, IVersusSettingsMenu, IMenuCancelHandler
    {
        [SerializeField]
        GameObject _FirstSelected;
        [SerializeField]
        EventSystem _EventSystem;
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

        public override void Show()
        {
            base.Show();

            _EventSystem?.SetSelectedGameObject(_FirstSelected.gameObject);

            versusData.settings = new VersusSettings(1, 60);
        }
    }
}