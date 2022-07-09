using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using CDR.UISystem;
using CDR.InputSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public class MapSelectMenu : VersusMenu, IMapSelectMenu, IMenuCancelHandler
    {
        [SerializeField]
        private SceneLoader _SceneLoader;
        [SerializeField]
        private GameObject _FirstSelect;
        [SerializeField]
        private EventSystem _EventSystem;

        public void OnCancel()
        {
            if(previousMenu != null)
                Back();
        }

        public void PickMap(IMapData mapData)
        {
            versusData.mapData = mapData;

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }

        public override void Show()
        {
            base.Show();

            _EventSystem.SetSelectedGameObject(_FirstSelect);
        }
    }
}