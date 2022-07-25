using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CDR.UISystem
{
    public class ProgressBar : MonoBehaviour, IValueRangeUI
    {
        [SerializeField] Image _Frame;
        [SerializeField] Image _fillImage;

        bool _isShown;
        public bool isShown => _isShown;

        public void Hide()
        {
            _Frame.enabled = false;
            _fillImage.enabled = false;
            _isShown = false;
        }

        public void Show()
        {
            _Frame.enabled = true;
            _fillImage.enabled = true;
            _isShown = true;
        }

        public void SetValueRange(IValueRange valueRange)
        {
            valueRange.OnModifyValue += OnModifyValue;
        }

        void OnModifyValue(IValueRange valueRange)
        {
            _fillImage.fillAmount = valueRange.CurrentValue / valueRange.MaxValue;
        }
    }
}
