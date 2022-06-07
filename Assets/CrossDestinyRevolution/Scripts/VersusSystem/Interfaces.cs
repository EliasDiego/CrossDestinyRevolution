using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public interface IParticipant
    {
        int score { get; set; }

        void Reset();
    }

    public interface IVersusData
    {
        IPlayerData player1Data { get; set; }
        IPlayerData player2Data { get; set; } 
        IMapData mapData { get; set; }
        IVersusSettings settings { get; set; }
        GameObject versusManagerPrefab { get; }
    }

    public interface IMechData
    {
        string mechName { get; }
        GameObject mechPrefab { get; }
        GameObject UIPrefab { get; }
    }

    public interface IMapData
    {
        string mapName { get; }
        GameObject mapPrefab { get; }
    }

    public interface IVersusSettings
    {
        public int rounds { get; }
        public int roundTime { get; }
    }

    public interface IPlayerInputData
    {
        InputUser user { get; set; }
    }

    public interface IPlayerData
    {
        InputDevice[] devices { get; set; }
        InputActionAsset actionAsset { get; set; }
        IMechData mechData { get; set; }
    }

    public interface IInputAssignmentMenu : IMenu
    {

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