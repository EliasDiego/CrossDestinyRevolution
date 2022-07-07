using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CDR.SceneManagementSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    SceneLoader _SceneLoader;
    [SerializeField]
    GameObject _GameObject;

    private struct TestTask : ISceneTask
    {
        public IEnumerator Process()
        {
            Scene scene = SceneManager.CreateScene("Versus Game Scene");

            yield return null;

            SceneManager.SetActiveScene(scene);
        }
    }

    public void TestF()
    {
        _SceneLoader.LoadSceneAsync(new TestTask());
    }
}