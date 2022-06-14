using System.Linq;
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

        // Rect GetRect(int cameras, int index)
        // {
        //     int oddFolds = Mathf.Max(cameras % 2 == 1 ? cameras : cameras - 1, 1);
        //     int evenFolds = Mathf.Max(cameras % 2 == 0 ? cameras : cameras - 1, 1);

        //     Vector2 cameraSize = new Vector2(1 / oddFolds, 1 / evenFolds);
            
        //     if(index > oddFolds)



        //     return new Rect(oddFolds * cameraSize.x, evenFolds * cameraSize.y, cameraSize.x, cameraSize.y);
        // }

        private Quaternion LookRotationTopDown(Vector3 direction)
        {
            return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
        }

        public IEnumerator Process()
        {
            yield return _VersusData.mapData.Process();

            GameObject.CreatePrimitive(PrimitiveType.Sphere);

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            IVersusMap versusMap = GameObject.Instantiate(_VersusData.versusMapPrefab, Vector3.zero, Quaternion.identity).GetComponent<IVersusMap>();

            IVersusUI versusUI = GameObject.Instantiate(_VersusData.versusUIPrefab).GetComponentInChildren<IVersusUI>();

            IParticipant[] participants = _VersusData.participantDatas.Select((p, i) => p.GetParticipant(versusMap.participantPositions[i], 
                Quaternion.LookRotation(versusMap.flightPlane.position.xz() - versusMap.participantPositions[i].xz(), Vector3.up), versusMap.flightPlane)).ToArray();

            // IParticipant player1 = _VersusData.player1Data.GetParticipant(versusMap.player1Position, Quaternion.LookRotation(versusMap.flightPlane.position - versusMap.player1Position, Vector3.up), versusMap.flightPlane);
            // IParticipant player2 = _VersusData.player2Data.GetParticipant(versusMap.player2Position, Quaternion.LookRotation(versusMap.flightPlane.position - versusMap.player2Position, Vector3.up), versusMap.flightPlane);

            versusUI.Hide();

            // if(player1 is ICameraParticipant)
            //     (player1 as ICameraParticipant).cameraRect = new Rect(Vector2.zero, new Vector2(0.5f, 1));
            
            // if(player2 is ICameraParticipant)
            //     (player2 as ICameraParticipant).cameraRect = new Rect(Vector2.right * 0.5f, new Vector2(0.5f, 1));

            yield return new WaitForSeconds(2);

            versusManager.Initialize(_VersusData.settings, versusUI, participants);
        }
    }
}