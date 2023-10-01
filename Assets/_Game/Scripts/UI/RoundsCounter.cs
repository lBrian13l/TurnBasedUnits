using UnityEngine;
using TMPro;

namespace TurnBasedUnits.UI
{
    public class RoundsCounter : MonoBehaviour, IUiUpdater
    {
        [SerializeField] private TextMeshProUGUI _uiCounter;

        public void UpdateUi(int value) => _uiCounter.text = $"Round {value}";
    }
}
