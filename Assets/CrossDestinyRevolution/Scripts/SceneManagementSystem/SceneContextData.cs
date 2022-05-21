using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.SceneManagementSystem
{
    public struct SceneContextData
    {
        private int _SceneIndex;
        private ISceneContextLoader _Loader;
        private ISceneContextUnloader _Unloader;

        public int sceneIndex => _SceneIndex;
        public ISceneContextLoader loader => _Loader;
        public ISceneContextUnloader unloader => _Unloader;

        public SceneContextData(int sceneIndex, ISceneContextLoader loader, ISceneContextUnloader unloader)
        {
            _SceneIndex = sceneIndex;
            _Loader = loader;
            _Unloader = unloader;
        }
    }
}