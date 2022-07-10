using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDR.VersusSystem
{
    [CreateAssetMenu(menuName = "VersusSystem/Map Data")]
    public class MapData : ScriptableObject, IMapData
    {
        [SerializeField]
        Sprite _Preview;
        [SerializeField]
        int _MapSceneIndex;

        public Sprite mapPreview => _Preview;

        public IEnumerator Process()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_MapSceneIndex, LoadSceneMode.Additive);

            while(!asyncOperation.isDone)
                yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_MapSceneIndex));
        }
    }
}