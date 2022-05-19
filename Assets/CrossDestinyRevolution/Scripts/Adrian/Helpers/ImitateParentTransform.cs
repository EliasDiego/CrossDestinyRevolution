using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class handles relative movement of parent and child transform without actual parenting of Child transform to
 * Parent transform.
 */

namespace CDR.Helpers
{
    public class ImitateParentTransform
    {
        private Transform parent;
        private Transform child;

        private Vector3 pos, forward, up;

        public ImitateParentTransform(Transform parentTrans, Transform childTrans)
        {
            parent = parentTrans;
            child = childTrans;

            pos = parent.transform.InverseTransformPoint(child.position);
            forward = parent.transform.InverseTransformDirection(child.forward);
            up = parent.transform.InverseTransformDirection(child.up);
        }


        public void MoveWithParent()
        {          
            var newPos = parent.transform.TransformPoint(pos);
            var newForward = parent.transform.TransformDirection(forward);
            var newUp = parent.transform.TransformDirection(up);
            var newRot = Quaternion.LookRotation(newForward, newUp);

            child.SetPositionAndRotation(newPos, newRot);
        }
    }
}
