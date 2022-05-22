using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDR.SceneManagementSystem
{
    public interface ISceneContextProcess
    {
        IEnumerator Process();
    }
    public interface ISceneContextLoader : ISceneContextProcess
    {
        
    }

    public interface ISceneContextUnloader : ISceneContextProcess
    {
        
    }

    public interface ISceneHandler
    {
        void LoadScene(LoadSceneMode loadSceneMode);
        AsyncOperation LoadSceneAsync(LoadSceneMode loadSceneMode);
        AsyncOperation UnloadSceneAsync(UnloadSceneOptions unloadSceneOptions);
    }
}