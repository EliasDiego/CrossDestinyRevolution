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
        SceneLoader _SceneLoader;

        public void OnCancel()
        {
            Back();
        }
        
        public void SetSettings()
        {
            versusData.settings = new VersusSettings(5, 20);

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }
    }
}