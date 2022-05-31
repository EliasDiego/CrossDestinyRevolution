using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
    public class HitBoxBase : MonoBehaviour
    {
        public Collider colliders;

        private IHitboxResponder _responder = null;

        public LayerMask mask;
        public bool useSphere = false;
        public Vector3 hitboxSize = Vector3.one;
        public float radius = 0.5f;

        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        private ColliderState _state = ColliderState.Open;

        private void Update()
        {
            hitboxUpdate();
        }

        public void hitboxUpdate()
        {
            if (_state == ColliderState.Closed) { return; }

            if (!useSphere)
            {
                Collider[] boxColliders = Physics.OverlapBox(transform.position, hitboxSize, transform.rotation, mask);

                for (int i = 0; i < boxColliders.Length; i++)
                {
                    Collider aCollider = boxColliders[i];
                    _responder?.collisionedWith(aCollider);
                }
                _state = boxColliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
            }

            if(useSphere)
			{
                Collider[] sphereColliders = Physics.OverlapSphere(transform.position, radius, mask);

                for (int i = 0; i < sphereColliders.Length; i++)
                {
                    Collider aCollider = sphereColliders[i];
                    _responder?.collisionedWith(aCollider);
                }
                _state = sphereColliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
            }
        }

        public void setResponder(IHitboxResponder responder)
        {
            _responder = responder;
        }

        public void startCheckingCollision()
        {
            _state = ColliderState.Open;
        }

        public void stopCheckingCollision()
        {
            _state = ColliderState.Closed;
        }

        void CheckGizmoColor()
        {
            switch (_state)
            {
                case ColliderState.Closed:

                    Gizmos.color = inactiveColor;
                    break;

                case ColliderState.Open:
                    Gizmos.color = collisionOpenColor;
                    break;

                case ColliderState.Colliding:
                    Gizmos.color = collidingColor;
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            CheckGizmoColor();

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

            if (!useSphere)
                Gizmos.DrawCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2));
            if (useSphere)
                Gizmos.DrawSphere(Vector3.zero, radius); // Because size is halfExtents
        }
    }
}

