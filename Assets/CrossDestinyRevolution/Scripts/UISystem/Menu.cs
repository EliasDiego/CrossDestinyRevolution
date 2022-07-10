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

        private static List<Menu> _Menus = new List<Menu>();

        public static Menu[] menus => _Menus.ToArray();

        protected virtual void OnEnable()
        {
            if(!_Menus.Contains(this))
                _Menus.Add(this);
        }

        protected virtual void OnDisable()
        {
            if(_Menus.Contains(this))
                _Menus.Remove(this);
        }

        protected virtual void Start() 
        {
            if(_IsShown)
                Show();
            else
                transform.SetActiveChildren(_IsShown);
        }

        private IEnumerator SwitchToCoroutine(IMenu nextMenu)
        {
            Hide();

            while(isShown)
                yield return null;

            nextMenu.Show();
        }

        public void Back()
        {
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(previousMenu));

            previousMenu = null;
        }

        public void SwitchTo(Menu menu)
        {
            menu.previousMenu = this;
            
            if(_SwitchToCoroutine != null)
                StopCoroutine(_SwitchToCoroutine);

            _SwitchToCoroutine = StartCoroutine(SwitchToCoroutine(menu));
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