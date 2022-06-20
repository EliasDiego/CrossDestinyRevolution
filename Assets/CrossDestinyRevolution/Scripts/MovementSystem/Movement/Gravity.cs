using System.Collections;
using UnityEngine;

// This class handles gravity simulation on a rigidbody.

namespace CDR.MovementSystem
{
    public class Gravity : MonoBehaviour
    {
        [Tooltip("Minimum y distance from flight plane before applying gravity")]
        [SerializeField]
        private float minDistToPull;
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private float pullSpeed = 12f;
        [SerializeField]
        private bool enableGravity = true;

        private void Start()
        {
            StartCoroutine(GravityOnRigidbody());
        }

        public void SetEnableGravity(bool isEnabled)
        {
            enableGravity = isEnabled;
        }

        // Enable gravity on object relative to flight plane.
        private IEnumerator GravityOnRigidbody()
        {
            while (true)
            {
                if (enableGravity && IsFarFromPlane())
                {
                    controller.AddRbForce(-transform.up * pullSpeed, ForceMode.Acceleration);
                }
                yield return null;
            }
        }

        private bool IsFarFromPlane()
        {
            return transform.position.y - controller.flightPlane.position.y > minDistToPull;
        }
    }
}
