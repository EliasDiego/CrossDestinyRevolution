using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField]
        Image _PreviewImage;

        protected override IEnumerator ShowAnimatedSequence()
        {
            yield return base.ShowAnimatedSequence();

            _EventSystem.SetSelectedGameObject(_FirstSelect);
        }

        public void OnCancel()
        {
            if(previousMenu != null)
                Back();
        }

        public void PreviewMap(Sprite preview)
        {
            _PreviewImage.sprite = preview;
        }

        public void PickMap(IMapData mapData)
        {
            versusData.mapData = mapData;

            _SceneLoader.LoadSceneAsync(new VersusSceneTask(versusData));
        }
    }
}