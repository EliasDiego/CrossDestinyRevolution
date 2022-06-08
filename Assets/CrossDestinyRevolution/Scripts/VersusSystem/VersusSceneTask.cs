using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

using CDR.UISystem;
using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;
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
            yield return _VersusData.mapData.Process();

            GameObject.CreatePrimitive(PrimitiveType.Sphere);

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            IVersusMap versusMap = GameObject.Instantiate(_VersusData.versusMap, Vector3.zero, Quaternion.identity).GetComponent<IVersusMap>();

            IParticipant player1 = _VersusData.player1Data.GetParticipant(versusMap.player1Position, versusMap.flightPlane);
            IParticipant player2 = _VersusData.player2Data.GetParticipant(versusMap.player2Position, versusMap.flightPlane);

            yield return null;

            player1.mech.controller.Rotate(Quaternion.LookRotation(player2.mech.position - player1.mech.position, Vector3.up));
            player2.mech.controller.Rotate(Quaternion.LookRotation(player1.mech.position - player2.mech.position, Vector3.up));

            if(player1 is ICameraParticipant)
                (player1 as ICameraParticipant).cameraRect = new Rect(Vector2.zero, new Vector2(0.5f, 1));
            
            if(player2 is ICameraParticipant)
                (player2 as ICameraParticipant).cameraRect = new Rect(Vector2.right * 0.5f, new Vector2(0.5f, 1));

            yield return new WaitForSeconds(2);

            versusManager.Initialize(_VersusData.settings, player1, player2);
        }
    }
}