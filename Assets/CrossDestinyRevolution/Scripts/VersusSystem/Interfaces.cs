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
    public interface ICameraParticipant : IParticipant
    {
        Camera camera { get; }
    }

    public interface IParticipant
    {
        int score { get; set; }
        
        IMech mech { get; }

        void Start();
        void Reset();
    }

    public interface IVersusMap : IMap
    {
        Vector3[] participantPositions { get; }
    }

    public interface IVersusData
    {
        IParticipantData[] participantDatas { get; }
        IMapData mapData { get; set; }
        IVersusSettings settings { get; set; }
        GameObject versusMapPrefab { get; }
        GameObject versusManagerPrefab { get; }
        GameObject versusUIPrefab { get; }
    }

    public interface IMechData
    {
        string mechName { get; }
        GameObject mechPrefab { get; }
    }

    public interface IMapData : ISceneTask
    {
        string mapName { get; }
    }

    public interface IVersusSettings
    {
        public int rounds { get; }
        public int roundTime { get; }
    }

    public interface IParticipantData
    { 
        IMechData mechData { get; set; }
        IParticipant GetParticipant(Vector3 startPosition, Quaternion startRotation, IFlightPlane flightPlane);
    }

    public interface IMechSelectMenu : IMenu
    {
        void PickMech(IPlayerInput playerInput, IMechData mechData);
    }

    public interface IMapSelectMenu : IMenu
    {
        void PickMap(IMapData mapData);
    }

    public interface IVersusSettingsMenu : IMenu
    {
        void SetSettings();
    }

    public interface IVersusUI : IUIElement
    {
        IRoundUIHandler roundUIHandler { get; }
        IRoundTimeUIHandler roundTimeUIHandler { get; }
    }

    public interface IRoundUIHandler : IUIElement
    {

    }

    public interface IRoundTimeUIHandler : IUIElement
    {
        string roundTimeText { get; set; }
    }
}