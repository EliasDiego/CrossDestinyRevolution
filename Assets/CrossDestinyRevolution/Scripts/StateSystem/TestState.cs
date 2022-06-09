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
        [SerializeField] Mech target;

        IMech receiver;
        IMech sender;

        private void Start()
        {
            receiver = mech.GetComponent<IMech>();
            sender = target.GetComponent<IMech>();

            receiver.currentState = knockbackPrefab.GetComponent<IState>();
            receiver.currentState.receiver = receiver;
            receiver.currentState.sender = sender;
            receiver.currentState.StartState();
        }
    }
}