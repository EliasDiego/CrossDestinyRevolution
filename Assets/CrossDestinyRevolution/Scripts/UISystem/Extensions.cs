using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public static class Extensions
    {
        public static void SetActiveChildren(this Transform transform, bool value)
        {
            for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}