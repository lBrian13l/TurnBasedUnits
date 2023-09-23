using UnityEngine;
using TMPro;

namespace TurnBasedUnits.UI
{
    public class RoundsCounter : MonoBehaviour, IUiUpdater
    {
        [SerializeField] private TextMeshProUGUI _uiCounter;

        private int _defaultValue = 1;

        public void Init() => UpdateUi(_defaultValue);

        public void UpdateUi(int value) => _uiCounter.text = $"Round {value}";
    }
}
