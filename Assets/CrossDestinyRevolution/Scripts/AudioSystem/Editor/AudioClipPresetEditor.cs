using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using CDR.AudioSystem;

namespace CDR.AudioSystemEditor
{
    [CustomEditor(typeof(AudioClipPreset))]
    public class AudioClipPresetEditor : Editor
    {
        AudioSource _PreviewAudioSource;

        private void OnEnable() 
        {
            _PreviewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();   
        }

        private void OnDisable() 
        {
            DestroyImmediate(_PreviewAudioSource.gameObject);    
        }

        public override void OnInspectorGUI()
        {
            DrawObjectField("_AudioClip", "Audio Clip");
            DrawObjectField("_OutputAudioMixerGroup", "Output Audio Mixer Group");
            DrawIntSliderField("_Priority", "Priority", 0, 256);

            DrawSliderField("_Volume", "Volume", 0, 1);

            bool isRandomPitch = DrawToggleField("_IsRandomPitch", "Random Pitch?");

            if(!isRandomPitch)
                DrawSliderField("_Pitch", "Pitch", -3, 3);

            else
            {
                SerializedProperty minPitchProperty = serializedObject.FindProperty("_MinPitch");
                SerializedProperty maxPitchProperty = serializedObject.FindProperty("_MaxPitch");

                float randomPitchMin = minPitchProperty.floatValue;
                float randomPitchMax = maxPitchProperty.floatValue;

                EditorGUILayout.MinMaxSlider("Pitch", ref randomPitchMin, ref randomPitchMax, -3, 3);
                
                EditorGUILayout.BeginHorizontal();
                randomPitchMin = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMin), -3, randomPitchMax);
                randomPitchMax = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMax), randomPitchMin, 3);
                EditorGUILayout.EndHorizontal();

                minPitchProperty.floatValue = randomPitchMin;
                maxPitchProperty.floatValue = randomPitchMax;
            }

            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Preview"))
                (target as AudioClipPreset).Play(_PreviewAudioSource);

            if(GUILayout.Button("Stop"))
                _PreviewAudioSource.Stop();

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        private bool DrawToggleField(string propertyName, string label)
        {
            SerializedProperty property = serializedObject.FindProperty(propertyName);

            property.boolValue = EditorGUILayout.Toggle(label, property.boolValue);

            return property.boolValue;
        }

        private void DrawObjectField(string propertyName, string label)
        {
            SerializedProperty property = serializedObject.FindProperty(propertyName);

            EditorGUILayout.ObjectField(property, new GUIContent(label));
        }

        private void DrawIntSliderField(string propertyName, string label, int min, int max)
        {
            SerializedProperty property = serializedObject.FindProperty(propertyName);

            property.intValue = EditorGUILayout.IntSlider(label, property.intValue, min, max);  
        }

        private void DrawSliderField(string propertyName, string label, float min, float max)
        {
            SerializedProperty property = serializedObject.FindProperty(propertyName);

            property.floatValue = EditorGUILayout.Slider(label, property.floatValue, min, max);           
        }
    }
}