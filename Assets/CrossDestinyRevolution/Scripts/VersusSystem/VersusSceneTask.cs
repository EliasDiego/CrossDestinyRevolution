using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using CDR.UISystem;
using CDR.MechSystem;
using CDR.InputSystem;
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

        private IParticipant SetupMechParticipant(IPlayerData playerData, Vector3 position)
        {
            IMech mech = GameObject.Instantiate(playerData.mechData.mechPrefab, position, Quaternion.identity).GetComponent<IMech>();

            Canvas canvas = GameObject.Instantiate(playerData.mechData.UIPrefab).GetComponent<Canvas>();

            IPlayerMechBattleUI battleUI = canvas.GetComponentInChildren<IPlayerMechBattleUI>();
            
            mech.input = InputUtilities.AssignPlayerInput<PlayerMechInput>((mech as Mech).gameObject, playerData.actionAsset.FindActionMap("Game", true), playerData.devices);
            
            // Debug
            Camera cam = (mech as Mech).GetComponentInChildren<Camera>();
            // Debug

            canvas.worldCamera = cam;

            return new Participant(mech, battleUI, cam, position);
        }

        public IEnumerator Process()
        {
            yield return _VersusData.mapData.Process();

            GameObject.CreatePrimitive(PrimitiveType.Sphere);

            VersusManager versusManager = GameObject.Instantiate(_VersusData.versusManagerPrefab).GetComponent<VersusManager>();
            
            IVersusMap versusMap = GameObject.Instantiate(_VersusData.versusMap, Vector3.zero, Quaternion.identity).GetComponent<IVersusMap>();

            IParticipant player1 = SetupMechParticipant(_VersusData.player1Data, versusMap.player1Position);
            IParticipant player2 = SetupMechParticipant(_VersusData.player2Data, versusMap.player2Position);

            yield return null;

            player1.mech.controller.Rotate(Quaternion.LookRotation(player2.mech.position - player1.mech.position, Vector3.up));
            player2.mech.controller.Rotate(Quaternion.LookRotation(player1.mech.position - player2.mech.position, Vector3.up));

            player1.camera.rect = new Rect(Vector2.zero, new Vector2(0.5f, 1));
            player2.camera.rect = new Rect(Vector2.right * 0.5f, new Vector2(0.5f, 1));

            yield return new WaitForSeconds(2);

            versusManager.Initialize(_VersusData.settings, player1, player2);
        }
    }
}