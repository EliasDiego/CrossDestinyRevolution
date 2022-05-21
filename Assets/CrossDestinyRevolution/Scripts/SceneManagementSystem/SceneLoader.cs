using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDR.SceneManagementSystem
{
    [CreateAssetMenu(menuName = "SceneManagementSystem/SceneLoader", fileName = "Scene Loader")]
    public class SceneLoader : ScriptableObject
    {
        [SerializeField]
        int _LoadingScreenSceneIndex;

        SceneContextData _SceneContextData;

        public int loadingScreenSceneIndex => _LoadingScreenSceneIndex;

        public SceneContextData sceneContextData => _SceneContextData;

        public void LoadSceneAsync(int sceneIndex)
        {
            LoadSceneAsync(null, null, sceneIndex);
        }

        public void LoadSceneAsync(ISceneContextUnloader unloader, ISceneContextLoader loader, int sceneIndex)
        {
            _SceneContextData = new SceneContextData(sceneIndex, loader, unloader);

            SceneManager.LoadScene(_LoadingScreenSceneIndex, LoadSceneMode.Additive);
        }
    }
}