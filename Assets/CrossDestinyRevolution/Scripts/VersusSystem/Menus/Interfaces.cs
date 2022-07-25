using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

using CDR.UISystem;
using CDR.InputSystem;

namespace CDR.VersusSystem
{
    public interface IPlayerInputSelectMenu : IMenu, IObserver<InputEventPtr>
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

    public interface IVersusResultsMenu : IMenu
    {
        IVersusResults results { get; set; }
        event Action rematchEvent;
        event Action returnToMainMenuEvent; 
    }

    public interface IVersusPauseMenu : IMenu
    {
        bool isPaused { get; }
        
        public IVersusUI versusUI { get; set; }
        public IPlayerMechBattleUI[] battleUIs { get; set; }
        public AudioSource musicAudioSource { get; set; }
        event Action returnToMainMenuEvent; 
        

        void EnablePauseInput();
        void DisablePauseInput();
    }
}