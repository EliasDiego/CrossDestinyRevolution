using System.Collections;
using System.Collections.Generic;

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
        public GameObject battleUIPrefab { get; set; }
        public InputActionAsset actionAsset { get; set; }
        public InputDevice[] devices { get; set; }

        public override IParticipant GetParticipant(Vector3 position, IFlightPlane flightPlane)
        {
            IParticipant participant = base.GetParticipant(position, flightPlane);

            GameObject battleUIObject = GameObject.Instantiate(battleUIPrefab);

            IPlayerMechBattleUI battleUI = battleUIObject.GetComponentInChildren<IPlayerMechBattleUI>();
            
            participant.mech.input = InputUtilities.AssignPlayerInput<PlayerMechInput>((participant.mech as Mech).gameObject, actionAsset.FindActionMap("Game", true), devices);
            
            // Debug
            Camera cam = (participant.mech as Mech).GetComponentInChildren<Camera>();
            // Debug

            UniversalAdditionalCameraData cameraData = cam.GetUniversalAdditionalCameraData();

            cameraData.cameraStack.Add(battleUIObject.GetComponentInChildren<Camera>());

            return new PlayerParticipant(participant.mech, battleUI, cam, position);
        }
    }
}