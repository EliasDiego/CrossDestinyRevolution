using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDR.SceneManagementSystem
{
    [CreateAssetMenu(menuName = "SceneManagementSystem/Scene Loader", fileName = "Scene Loader")]
    public class SceneLoader : ScriptableObject
    {
        [SerializeField]
        int _LoadingScreenSceneIndex;

        private ISceneTask _SceneTask;

        public int loadingScreenSceneIndex => _LoadingScreenSceneIndex;

        public ISceneTask sceneTask => _SceneTask;

        private struct SimpleSceneTask : ISceneTask
        {
            private int _SceneIndex;

            public SimpleSceneTask(int sceneIndex)
            {
                _SceneIndex = sceneIndex;
            }

            public IEnumerator Process()
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_SceneIndex, LoadSceneMode.Additive);

                yield return new WaitWhile(() => asyncOperation.isDone);
                
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_SceneIndex));
            }
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadSceneAsync(ISceneTask sceneTask)
        {
            _SceneTask = sceneTask;

            SceneManager.LoadScene(_LoadingScreenSceneIndex, LoadSceneMode.Additive);
        }

        public void LoadSceneAsync(int sceneIndex)
        {
            LoadSceneAsync(new SimpleSceneTask(sceneIndex));
        }
    }
}