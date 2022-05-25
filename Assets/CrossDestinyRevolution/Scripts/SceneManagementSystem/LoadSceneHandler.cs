using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

namespace CDR.SceneManagementSystem
{
    public class LoadSceneHandler : MonoBehaviour
    {
        [SerializeField]
        SceneLoader _SceneLoader;
        [SerializeField]
        Camera _Camera;
        
        private void Start() 
        {
            StartCoroutine(Sequence());    
        }

        private IEnumerator Sequence()
        {
            _Camera.gameObject.SetActive(false);

            // Unload Previous Scene
            yield return UnloadScene(SceneManager.GetActiveScene());

            _Camera.gameObject.SetActive(true);

            yield return new WaitForSeconds(1);

            // Use Scene task
            yield return _SceneLoader.sceneTask.Process();

            yield return new WaitForSeconds(1);

            // // Unload Loading Screen
            yield return UnloadScene(SceneManager.GetSceneByBuildIndex(_SceneLoader.loadingScreenSceneIndex));
        }
        
        // private IEnumerator Sequence()
        // {
        //     _Camera.gameObject.SetActive(false);

        //     // Unload Previous Scene
        //     yield return UnloadScene(SceneManager.GetActiveScene());

        //     _Camera.gameObject.SetActive(true);

        //     yield return new WaitForSeconds(1);

        //     // Create Temp Scene
        //     Scene tempScene = SceneManager.CreateScene("Temp Scene");

        //     SceneManager.SetActiveScene(tempScene);

        //     yield return new WaitForSeconds(1);

        //     // Load Next Scene

        //     yield return LoadScene(contextData.sceneIndex, LoadSceneMode.Additive, OnBeforeSceneActivation);

        //     yield return new WaitForSeconds(1);

        //     Scene nextScene = SceneManager.GetSceneByBuildIndex(contextData.sceneIndex);

        //     SceneManager.SetActiveScene(nextScene);

        //     foreach(GameObject gameObject in tempScene.GetRootGameObjects())
        //         SceneManager.MoveGameObjectToScene(gameObject, nextScene);

        //     yield return UnloadScene(tempScene);

        //     yield return new WaitForSeconds(1);

        //     // // Unload Loading Screen
        //     yield return UnloadScene(SceneManager.GetSceneByBuildIndex(_SceneLoader.loadingScreenSceneIndex));
        // }

        public IEnumerator UnloadScene(Scene scene)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(scene);

            while(!asyncOperation.isDone)
                yield return null;
        }
    }
}