using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private BoostInfo boostInfo;
        [SerializeField]
        private ValueRange BoostValue;
        [SerializeField]
        private float MoveSpeed;

        public Boost Boost { get; private set; }

        private RbMovement movement;
        private Rigidbody rb;
     
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Boost = new Boost(boostInfo, this, BoostValue);
            movement = new RbMovement(MoveSpeed, transform, rb);
        }

        private void OnEnable()
        {
            Boost.OnUse += movement.DisableClamp;
        }

        private void OnDisable()
        {
            Boost.OnUse -= movement.DisableClamp;           
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                UseBoost();
            }
        }

        // Move controller with input.
        // TODO: Replace with better input system.
        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            movement.Move(direction);
        }

        public void UseBoost()
        {
            StartCoroutine(Boost.Use(transform.forward, rb));
        }
    }
}

