using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.UI
{
    public class Bar : MonoBehaviour, IUiUpdater
    {
        [SerializeField] private Image _filledImage;
        [SerializeField] private TextMeshProUGUI _numericValue;
        [SerializeField] private StatType _type;

        private int _maxValue = 100;

        public StatType Type => _type;

        public void Init() => UpdateUi(_maxValue);

        public void UpdateUi(int value)
        {
            _filledImage.fillAmount = value / _maxValue;
            _numericValue.text = value.ToString();
        }
    }
}
