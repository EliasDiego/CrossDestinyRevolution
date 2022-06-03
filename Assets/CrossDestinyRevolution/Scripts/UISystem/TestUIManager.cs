using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem; 

namespace CDR.UISystem
{
    public class TestUIManager : MonoBehaviour
    {
        [SerializeReference]
        GameObject _BattleUIObject;
        [SerializeField]
        Mech _Mech;

        private void Start() 
        {
            if(!_BattleUIObject)
                return;

            if(_BattleUIObject.TryGetComponent<IPlayerMechBattleUI>(out IPlayerMechBattleUI battleUI))
            {
                battleUI.SetMech(_Mech);
                battleUI.Show();    
            }
        }
    }
}