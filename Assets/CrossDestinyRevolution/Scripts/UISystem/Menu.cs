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

        protected virtual void Start() 
        {
            if(_IsShown)
                Show();

            else
                Hide();
            // transform.SetActiveChildren();
        }

        private IEnumerator SwitchToCoroutine(IMenu nextMenu)
        {
            Hide();

            while(isShown)
                yield return null;

            nextMenu.Show();

            Debug.Log(nextMenu);
        }

        public void Back()
        {
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(previousMenu));

            previousMenu = null;
        }

        public void SwitchTo(IMenu nextMenu)
        {
            nextMenu.previousMenu = this;
            
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(nextMenu));
        }

        public void SwitchTo(Menu menu)
        {
            SwitchTo((IMenu)menu);
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