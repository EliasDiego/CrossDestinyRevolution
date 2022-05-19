using System;
using UnityEngine;
using CDR.Helpers;

// This class handles FlightPlane methods and events for resize, move, and rotate.

namespace CDR.MovementSystem
{
    public class FlightPlane : MonoBehaviour
    {
        public bool enableImitate = false;

        private ImitateParentTransform[] children;
        private int currentIndex = 0;
        private Renderer _renderer;
      
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            children = new ImitateParentTransform[2];
        }

        private void Update()
        {
            if(enableImitate)
                children[0].MoveWithParent();
        }

        public void AddChild(Transform childToAdd)
        {
            children[currentIndex] = new ImitateParentTransform(transform, childToAdd);
            currentIndex++;          
        }

        public bool IsObjectInBounds(Vector3 point)
        {
            return _renderer.bounds.Contains(new Vector3(point.x, transform.position.y, point.z));
        }
    }
}

