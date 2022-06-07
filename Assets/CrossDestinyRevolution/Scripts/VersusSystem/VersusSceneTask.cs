using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CDR.UISystem;
using CDR.MechSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public struct VersusSceneTask : ISceneTask
    {
        private IVersusData _VersusData;

        public VersusSceneTask(IVersusData versusData)
        {
            _VersusData = versusData;
        }

        private IMech AssignMech(IMech mech, IPlayerMechBattleUI battleUI) // Map, Camera
        {
            battleUI.SetMech(mech);

            return mech;
        }

        public IEnumerator Process()
        {
            Scene versusScene = SceneManager.CreateScene("Versus Scene");
            
            SceneManager.SetActiveScene(versusScene);
            
            Debug.Log(_VersusData + " | " + _VersusData?.player1Data + " | " + _VersusData?.player1Data?.mechData + " | " + _VersusData?.player1Data?.mechData?.mechPrefab);

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            GameObject.Instantiate(_VersusData.mapData.mapPrefab);

            // IMech player1Mech = AssignMech(GameObject.Instantiate(_VersusData.player1Data.mechData.mechPrefab).GetComponent<IMech>(), GameObject.Instantiate(_VersusData.player1Data.mechData.UIPrefab).GetComponent<IPlayerMechBattleUI>(), )
            
            GameObject.Instantiate(_VersusData.player2Data.mechData.mechPrefab);
            GameObject.Instantiate(_VersusData.player2Data.mechData.UIPrefab);

            versusManager.Initialize(_VersusData.settings);

            yield return null;
        }
    }
}