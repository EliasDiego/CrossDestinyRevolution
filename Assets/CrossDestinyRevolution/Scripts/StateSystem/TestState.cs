using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.MechSystem;

namespace CDR.StateSystem
{
    public class TestState : MonoBehaviour
    {
        [SerializeField] GameObject stunPrefab;
        [SerializeField] GameObject knockbackPrefab;

        [SerializeField] Mech mech;
        [SerializeField] Mech sender;

        private void Start()
        {
            mech.currentState = stunPrefab.GetComponent<IState>();

            mech.currentState.receiver = mech;
            mech.currentState.sender = sender;
            mech.currentState.StartState();
        }
    }
}