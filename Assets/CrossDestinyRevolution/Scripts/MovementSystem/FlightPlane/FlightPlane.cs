using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using CDR.VFXSystem;

// This class handles FlightPlane methods and events for resize, move, and rotate.
// NOTE: To ensure that radius works properly, make sure that x and z scale of flightPlane is similar

namespace CDR.MovementSystem
{
    public class FlightPlane : MonoBehaviour, IFlightPlane
    {
        [SerializeField]
        BoundsVFXHandler _BoundsVFXHandler;
        private List<CharacterController>_activeControllers = new List<CharacterController>();

        public List<CharacterController> activeControllers => _activeControllers;

        public float radius => CalculateRadius();

        Vector3 IFlightPlane.position => transform.position;
        Quaternion IFlightPlane.rotation => transform.rotation;

        private void Start() 
        {
            _BoundsVFXHandler.Activate();
        }

        public void AddCharacterController(CharacterController controller)
        {
            if(!_activeControllers.Contains(controller))
            {
                _activeControllers.Add(controller);

                _BoundsVFXHandler.trackedTransforms = _activeControllers.Select(c => c.transform).ToArray();
            }
        }

        private float CalculateRadius()
        {
            return transform.localScale.x / 2f;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, 
                new Vector3(transform.localScale.x, 0f, 0f) / 2f, 
                Color.red, 99999f, false);
        }
    }
}

