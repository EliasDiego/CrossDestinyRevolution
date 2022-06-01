using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class VersusSettingsMenu : VersusMenu, IVersusSettingsMenu
    {
        [SerializeField]
        SceneLoader _SceneLoader;

        public override void Show()
        {
            base.Show();

            player1Input.EnableInput();
            player2Input.EnableInput();
        }

        public void SetSettings()
        {
            versusData.settings = new VersusSettings(5, 20);

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }
    }
}