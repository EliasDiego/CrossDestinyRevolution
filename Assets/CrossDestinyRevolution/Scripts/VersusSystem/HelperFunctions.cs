using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.VersusSystem
{
    public static class HelperFunctions
    {
        public static Vector3 xz(this Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }

        public static Vector3 xy(this Vector3 source)
        {
            return new Vector3(source.x, source.y, 0);
        }
    }
}