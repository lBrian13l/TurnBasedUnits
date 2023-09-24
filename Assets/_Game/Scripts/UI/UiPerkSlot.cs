using UnityEngine;
using TMPro;

namespace TurnBasedUnits.UI
{
    public class UiPerkSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buffText;

        public void Init()
        {
            Clear();
        }

        public void UpdateSlot(string perkName, int turnsCount)
        {
            _buffText.text = $"{perkName} ({turnsCount})";
        }

        public void Clear()
        {
            _buffText.text = "";
        }
    }
}
