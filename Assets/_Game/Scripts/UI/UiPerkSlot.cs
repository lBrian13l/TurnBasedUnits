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

        public void UpdateSlot(int perkID, int turnsCount)
        {
            
        }

        public void Clear()
        {
            _buffText.text = "";
        }
    }
}
