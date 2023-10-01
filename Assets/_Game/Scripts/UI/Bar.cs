using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TurnBasedUnits.Helpers;
using DG.Tweening;

namespace TurnBasedUnits.UI
{
    public class Bar : MonoBehaviour, IUiUpdater
    {
        [SerializeField] private Image _filledImage;
        [SerializeField] private TextMeshProUGUI _numericValue;
        [SerializeField] private StatType _type;

        private float _maxValue;

        public StatType Type => _type;

        public void Init(int maxValue) => _maxValue = maxValue;

        public void UpdateUi(int value)
        {
            _filledImage.DOFillAmount(value / _maxValue, 0.25f);
            _numericValue.text = value.ToString();
        }
    }
}
