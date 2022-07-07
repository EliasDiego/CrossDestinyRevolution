using System.Collections;
using System.Collections.Generic;
using CDR.ActionSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CDR.UISystem
{
    public class CooldownActionUI : MonoBehaviour, ICooldownActionUI
    {
        [SerializeField] Image _fillImage;

        bool _isShown;
        public bool isShown => _isShown;

        public void Hide()
        {
            _fillImage.enabled = false;
            _isShown = false;
        }

        public void Show()
        {
            _fillImage.enabled = true;
            _isShown = true;
        }

        public void SetCooldownAction(ICooldownAction cooldownAction)
        {
            cooldownAction.onStartCoolDown += OnStartCoolDown;
            cooldownAction.onCoolDown += OnCooldownChange;
            cooldownAction.onEndCoolDown += OnEndCooldown;
        }

        void OnStartCoolDown(ICooldownAction cooldownAction)
        {
            _fillImage.fillAmount = 1;
        }

        void OnCooldownChange(ICooldownAction cooldownAction)
        {
            _fillImage.fillAmount = cooldownAction.currentCooldown / cooldownAction.cooldownDuration;
        }

        void OnEndCooldown(ICooldownAction cooldownAction)
        {
            _fillImage.fillAmount = 1;
        }
    }
}