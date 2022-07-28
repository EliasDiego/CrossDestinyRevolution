using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/AfterVersusSceneTask")]
    public class AfterVersusSceneTask : ScriptableObject, ISceneTask
    {
        [SerializeField]
        int _NextSceneIndex;

        public IEnumerator Process()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_NextSceneIndex, LoadSceneMode.Additive);

            LeanTween.cancelAll();

            while(!asyncOperation.isDone)
                yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_NextSceneIndex));
        }
    }
}