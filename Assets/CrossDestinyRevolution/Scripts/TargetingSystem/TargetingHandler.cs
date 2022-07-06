using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CDR.ActionSystem;
using CDR.MechSystem;

namespace CDR.TargetingSystem
{
    public class TargetingHandler : Action, ITargetHandler
    {
        ActiveCharacter currentTarget;
        public event System.Action<ITargetData> onSwitchTarget;

        private void Start() 
        {

        }

        public override void Use()
        {
            base.Use();

            currentTarget = ActiveCharacter.activeCharacters.Where(a => (a != (ActiveCharacter)Character))?
                .OrderBy(a => Vector3.Distance(Character.position, a.transform.position))?.FirstOrDefault();

            onSwitchTarget?.Invoke(new TargetData(currentTarget, 
                Vector3.Distance(currentTarget.transform.position, Character.position),
                (Character.position - currentTarget.transform.position).normalized));
        }

        public override void End()
        {
            base.End();
        }

        public ITargetData GetCurrentTarget()
        {
            return new TargetData(currentTarget, 
                Vector3.Distance(currentTarget.transform.position, Character.position),
                (Character.position - currentTarget.transform.position).normalized);
        }

        public void GetNextTarget()
        {
            IEnumerable<ActiveCharacter> activeCharacters = ActiveCharacter.activeCharacters?.Where(a => (a != (ActiveCharacter)Character && a != currentTarget))
                ?.OrderBy(a => Vector3.Distance(Character.position, a.transform.position));

            ActiveCharacter nextTarget = activeCharacters?.FirstOrDefault();
            if(nextTarget == null)
            {
                return;
            }

            

            onSwitchTarget?.Invoke(new TargetData(nextTarget, 
                Vector3.Distance(nextTarget.transform.position, Character.position),
                (Character.position - nextTarget.transform.position).normalized));

            currentTarget = nextTarget;

            Debug.Log("Change Target" + nextTarget.name);
        }
    }
}