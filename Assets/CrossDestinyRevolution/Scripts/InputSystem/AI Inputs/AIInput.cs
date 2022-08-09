using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public abstract class AIInput : MonoBehaviour, IAIInput
    {
        [SerializeField]
        protected Dictionary<string, AIAction> _AIActions = new Dictionary<string, AIAction>();

        public event Action<IInput> onEnableInput;
        public event Action<IInput> onDisableInput;

        protected virtual void Update()
        {
            foreach(AIAction action in _AIActions?.Values?.Where(a => a.enabled))
                action.Invoke();
        }

        protected void AddAction(string name, Action action)
        {
            if(!_AIActions.ContainsKey(name))
                _AIActions.Add(name, new AIAction(name, action));
        }

        protected void RemoveAction(string name)
        {
            if(_AIActions.ContainsKey(name))
                _AIActions.Remove(name);
        }

        public void DisableInput()
        {
            foreach(AIAction action in _AIActions?.Values?.Where(a => a.enabled))
                action.enabled = false;

            onDisableInput?.Invoke(this);
        }

        public void DisableInput(string name)
        {
            if(_AIActions.ContainsKey(name))
                _AIActions[name].enabled = false;

            onDisableInput?.Invoke(this);
        }

        public void DisableInputExcept(params string[] except)
        {
            foreach(AIAction action in _AIActions?.Values?.Where(i => !except.Contains(i.name)))
                action.enabled = false;

            onDisableInput?.Invoke(this);
        }

        public void EnableInput()
        {
            foreach(AIAction action in _AIActions?.Values?.Where(a => !a.enabled))
                action.enabled = true;

            onEnableInput?.Invoke(this);
        }

        public void EnableInput(string name)
        {
            if(_AIActions.ContainsKey(name))
                _AIActions[name].enabled = true;

            onEnableInput?.Invoke(this);
        }

        public void EnableInputExcept(params string[] except)
        {
            foreach(AIAction action in _AIActions?.Values?.Where(i => !except.Contains(i.name)))
                action.enabled = true;

            onEnableInput?.Invoke(this);
        }

        public abstract void SetupInput();
    }
}