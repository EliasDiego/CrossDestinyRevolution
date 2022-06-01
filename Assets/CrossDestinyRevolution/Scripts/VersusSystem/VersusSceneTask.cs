using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

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

        public IEnumerator Process()
        {
            Scene versusScene = SceneManager.CreateScene("Versus Scene");
            
            SceneManager.SetActiveScene(versusScene);
            
            Debug.Log(_VersusData + " | " + _VersusData?.player1Data + " | " + _VersusData?.player1Data?.mechData + " | " + _VersusData?.player1Data?.mechData?.mechPrefab);
            GameObject.Instantiate(_VersusData.player1Data.mechData.mechPrefab);
            // GameObject.Instantiate(_VersusData.player2Data.mechData.mechPrefab);
            GameObject.Instantiate(_VersusData.mapData.mapPrefab);

            yield return null;
        }
    }
}