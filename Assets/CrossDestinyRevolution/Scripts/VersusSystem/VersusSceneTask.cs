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

        private IMech InitializeMech(GameObject mechPrefab, GameObject battleUIPrefab, Vector3 position)
        {
            IMech mech = GameObject.Instantiate(mechPrefab, position, Quaternion.identity).GetComponent<IMech>();

            // IPlayerMechBattleUI bats

            return mech;
        }

        public IEnumerator Process()
        {
            yield return _VersusData.mapData.Process();

            GameObject.CreatePrimitive(PrimitiveType.Sphere);

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            IVersusMap versusMap = GameObject.Instantiate(_VersusData.versusMap, Vector3.zero, Quaternion.identity).GetComponent<IVersusMap>();

            IMech player1Mech = InitializeMech(_VersusData.player1Data.mechData.mechPrefab, _VersusData.player1Data.mechData.UIPrefab, versusMap.player1Position);
            IMech player2Mech = InitializeMech(_VersusData.player2Data.mechData.mechPrefab, _VersusData.player2Data.mechData.UIPrefab, versusMap.player2Position);

            // versusManager.Initialize(_VersusData.settings, player1Mech, player2Mech);

            yield return null;
        }
    }
}