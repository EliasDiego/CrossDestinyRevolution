using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

using CDR.SceneManagementSystem;

namespace CDR.SceneManagementSystemEditor
{
    [CustomEditor(typeof(SceneHandler))]
    public class SceneHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty sceneIndexProperty = serializedObject.FindProperty("_SceneIndex");
        }
    }
}