using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

using CDR.UISystem;
using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;

namespace CDR.VersusSystem
{
    public class PlayerParticipantData : ParticipantData
    {
        public GameObject cameraPrefab { get; set; }
        public GameObject battleUIPrefab { get; set; }
        public InputActionAsset actionAsset { get; set; }
        public InputDevice[] devices { get; set; }
        public PlayerMechInputSettings settings { get; set; }

        public override IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane)
        {
            IParticipant participant = base.GetParticipant(startPosition, startRotation, flightPlane);

            GameObject battleUIObject = GameObject.Instantiate(battleUIPrefab);

            IPlayerMechBattleUI battleUI = battleUIObject.GetComponentInChildren<IPlayerMechBattleUI>();
            
            GameObject cameraObject = GameObject.Instantiate(cameraPrefab);

            Camera cam = cameraObject.GetComponent<Camera>();

            UniversalAdditionalCameraData cameraData = cam.GetUniversalAdditionalCameraData();

            cameraData.cameraStack.Add(battleUIObject.GetComponentInChildren<Camera>());
            
            PlayerMechInput playerInput = InputUtilities.AssignPlayerInput<PlayerMechInput>((participant.mech as Mech).gameObject, actionAsset.FindActionMap("Game", true), devices);

            playerInput.settings = settings;
            
            participant.mech.input = playerInput;

            return new PlayerParticipant(participant.name, participant.mech, battleUI, cam, startPosition, startRotation);
        }
    }
}