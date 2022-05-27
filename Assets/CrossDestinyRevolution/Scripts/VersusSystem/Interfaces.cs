using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public interface IVersusData
    {
        IPlayerData player1Data { get; set; }
        IPlayerData player2Data { get; set; } 
        IMapData mapData { get; set; }
        IVersusSettings settings { get; set; }
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
        InputUser user { get; set; }
        IMechData mechData { get; set; }
    }

    public interface IInputAssignmentMenu : IMenu
    {

    }

    public interface IMechSelectMenu : IMenu
    {
        void PickMech(int player, IMechData mechData);
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