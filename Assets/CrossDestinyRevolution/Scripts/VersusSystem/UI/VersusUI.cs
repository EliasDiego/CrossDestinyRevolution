using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class VersusUI : MonoBehaviour, IVersusUI
    {
        [SerializeField]
        RoundUIHandler _RoundUIHandler;
        [SerializeField]
        RoundTimeUIHandler _RoundTimeUIHandler;
        [SerializeField]
        VersusResultsMenu _VersusResultsMenu;
        [SerializeField]
        VersusPauseMenu _PauseMenu;

        public IRoundUIHandler roundUIHandler => _RoundUIHandler;
        public IRoundTimeUIHandler roundTimeUIHandler => _RoundTimeUIHandler;
        public IVersusResultsMenu resultsMenu => _VersusResultsMenu;
        public IVersusPauseMenu pauseMenu => _PauseMenu;
    }
}