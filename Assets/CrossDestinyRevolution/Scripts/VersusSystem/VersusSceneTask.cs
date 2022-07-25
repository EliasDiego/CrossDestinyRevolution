using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

using CDR.UISystem;
using CDR.MechSystem;
using CDR.InputSystem;
using CDR.CameraSystem;
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

        Rect GetRect(int cameras, int playerNumber)
        {
            int folds = Mathf.RoundToInt(Mathf.Pow(1, cameras));
            // int oddFolds = Mathf.Max(cameras % 2 == 1 ? cameras : cameras - 1, 1);
            // int evenFolds = Mathf.Max(cameras % 2 == 0 ? cameras : cameras - 1, 1);

            Vector2 cameraSize = Vector2.one / folds;

            return new Rect(playerNumber * cameraSize.x, playerNumber * cameraSize.y, cameraSize.x, cameraSize.y);
        }

        private Quaternion LookRotationTopDown(Vector3 direction)
        {
            return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
        }

        public IEnumerator Process()
        {
            yield return _VersusData.mapData.Process();

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            IVersusMap versusMap = GameObject.Instantiate(_VersusData.versusMapPrefab, Vector3.zero, Quaternion.identity).GetComponent<IVersusMap>();

            IVersusUI versusUI = GameObject.Instantiate(_VersusData.versusUIPrefab).GetComponentInChildren<IVersusUI>();

            IParticipant[] participants = _VersusData.participantDatas.Select((p, i) => p.GetParticipant(versusMap.participantPositions[i], 
                Quaternion.LookRotation(versusMap.flightPlane.position.xz() - versusMap.participantPositions[i].xz(), Vector3.up), versusMap.flightPlane)).ToArray();

            versusUI.pauseMenu.versusUI = versusUI;
            versusUI.pauseMenu.battleUIs = participants?.Cast<PlayerParticipant>()?.Select(p => p.battleUI)?.ToArray();
            versusUI.pauseMenu.musicAudioSource = versusManager.GetComponentInChildren<AudioSource>();

            ICameraParticipant[] cameraParticipants = participants.Where(p => p is ICameraParticipant).Cast<ICameraParticipant>().ToArray();

            cameraParticipants[0].camera.cullingMask ^= LayerMask.GetMask("Player2Cam");
            cameraParticipants[0].camera.rect = new Rect(Vector2.zero, new Vector2(0.5f, 1));

            cameraParticipants[1].camera.cullingMask ^= LayerMask.GetMask("Player1Cam");
            cameraParticipants[1].camera.rect = new Rect(Vector2.right * 0.5f, new Vector2(0.5f, 1));

            CameraManager cameraManager = GameObject.Instantiate(_VersusData.cameraManagerPrefab).GetComponent<CameraManager>();

            cameraManager.gameObject.SetActive(false);

            yield return null;

            cameraManager.gameObject.SetActive(true);

            for(int i = 0; i < cameraParticipants.Length; i++)
                cameraManager.SetPlayerCam((cameraParticipants[i].mech as Mech).transform, i);

            yield return new WaitForSeconds(2);

            versusManager.Initialize(_VersusData.settings, versusUI, participants);
        }
    }
}