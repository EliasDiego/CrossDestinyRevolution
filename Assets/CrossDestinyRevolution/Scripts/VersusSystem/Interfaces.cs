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
        Rect cameraRect { set; }
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
        Vector3 player1Position { get; }
        Vector3 player2Position { get; }
    }

    public interface IVersusData
    {
        IParticipantData player1Data { get; set; }
        IParticipantData player2Data { get; set; } 
        IMapData mapData { get; set; }
        IVersusSettings settings { get; set; }
        GameObject versusMap { get; }
        GameObject versusManagerPrefab { get; }
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
}