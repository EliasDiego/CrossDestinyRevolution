using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDR.SceneManagementSystem
{
    [CreateAssetMenu(menuName = "SceneManagementSystem/Scene Handler")]
    public class SceneHandler : ScriptableObject, ISceneHandler
    {
        [SerializeField, HideInInspector]
        int _SceneIndex;

        public void LoadScene(LoadSceneMode loadSceneMode)
        {
            SceneManager.LoadScene(_SceneIndex, loadSceneMode);
        }

        public AsyncOperation LoadSceneAsync(LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(_SceneIndex, loadSceneMode);
        }

        public AsyncOperation UnloadSceneAsync(UnloadSceneOptions unloadSceneOptions)
        {
            return SceneManager.UnloadSceneAsync(_SceneIndex, unloadSceneOptions);
        }
    }
}