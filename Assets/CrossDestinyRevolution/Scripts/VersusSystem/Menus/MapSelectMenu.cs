using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.UISystem;
using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class MapSelectMenu : VersusMenu, IMapSelectMenu, IMenuCancelHandler
    {
        [SerializeField]
        private SceneLoader _SceneLoader;

        public void OnCancel()
        {
            Back();
        }

        public void PickMap(IMapData mapData)
        {
            versusData.mapData = mapData;

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }
    }
}