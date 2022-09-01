using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.UISystem;
using CDR.MapSystem;
using CDR.MechSystem;
using CDR.InputSystem;
using CDR.MovementSystem;
using CDR.SceneManagementSystem;

namespace CDR.VersusSystem
{
    public interface IVersusMap : IMap
    {
        Vector3[] participantPositions { get; }
    }

    public interface IVersusData
    {
        IParticipantData[] participantDatas { get; }
        IMapData mapData { get; }
        IVersusSettings settings { get; }
        GameObject versusMapPrefab { get; }
        GameObject versusManagerPrefab { get; }
        GameObject versusUIPrefab { get; }
        GameObject cameraManagerPrefab { get; }
    }

    public interface IMechData
    {
        string mechName { get; }
        GameObject mechPrefab { get; }
    }

    public interface IMapData : ISceneTask
    {
        
    }

    public interface IVersusSettings
    {
        public int rounds { get; }
        public int roundTime { get; }
    }

    public interface IVersusResults
    {
        string winnerText { get; }
        IParticipant[] participants { get; }
    }

    public interface IVersusUI
    {
        IRoundUIHandler roundUIHandler { get; }
        IRoundTimeUIHandler roundTimeUIHandler { get; }
        IVersusResultsMenu resultsMenu { get; }
        IVersusPauseMenu pauseMenu { get; }
    }

    public interface IRoundUIHandler : IUIElement
    {

    }

    public interface IRoundTimeUIHandler : IUIElement
    {
        string roundTimeText { get; set; }
    }
}