using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}