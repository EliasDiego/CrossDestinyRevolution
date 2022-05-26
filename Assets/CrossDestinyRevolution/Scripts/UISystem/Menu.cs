using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class Menu : MonoBehaviour, IMenu
    {
        [SerializeField]
        private bool _IsShown = false;
        private Coroutine _SwitchToCoroutine;

        public IMenu previousMenu { get; set; }
        public bool isShown { get => _IsShown; protected set => _IsShown = value; }

        protected virtual void Awake() 
        {
            if(_IsShown)
                transform.SetActiveChildren(true);
        }

        private IEnumerator SwitchToCoroutine(IMenu previousMenu, IMenu nextMenu)
        {
            previousMenu.Hide();

            while(!isShown)
                yield return null;

            nextMenu.Show();
        }

        public virtual void Back()
        {
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(this, previousMenu));
            
            previousMenu = null;
        }

        public virtual void SwitchTo(IMenu nextMenu)
        {
            nextMenu.previousMenu = this;
            
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(this, nextMenu));
        }

        public void SwitchTo(Menu menu)
        {
            SwitchTo(menu as IMenu);
        }

        public virtual void Hide()
        {
            isShown = false;

            transform.SetActiveChildren(false);
        }

        public virtual void Show()
        {
            isShown = true;

            transform.SetActiveChildren(true);
        }
    }
}