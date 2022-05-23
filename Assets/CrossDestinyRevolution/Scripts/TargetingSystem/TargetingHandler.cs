using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            currentTarget = ActiveCharacter.activeCharacters.Where(a => (a != Character))
                .OrderBy(a => Vector3.Distance(Character.transform.position, a.transform.position)).FirstOrDefault();
                
        }

        public override void End()
        {
            base.End();
        }

        public ITargetData GetCurrentTarget()
        {
            return new TargetData(currentTarget, 
                Vector3.Distance(currentTarget.transform.position, Character.transform.position),
                (Character.transform.position - currentTarget.transform.position).normalized);
        }

        public void GetNextTarget()
        {
            ActiveCharacter nextTarget = ActiveCharacter.activeCharacters.Where(a => (a != Character && a != currentTarget))
                .OrderBy(a => Vector3.Distance(Character.transform.position, a.transform.position)).FirstOrDefault();

            onSwitchTarget?.Invoke(new TargetData(nextTarget, 
                Vector3.Distance(nextTarget.transform.position, Character.transform.position),
                (Character.transform.position - nextTarget.transform.position).normalized));

            currentTarget = nextTarget;
        }
    }
}