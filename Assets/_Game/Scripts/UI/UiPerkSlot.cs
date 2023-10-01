using UnityEngine;
using TMPro;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.UI
{
    public class UiPerkSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buffText;

        private PerkName _name;

        public PerkName Name => _name;

        public void Restart() => Clear();

        public void UpdateSlot(PerkName perkName, int duration)
        {
            _name = perkName;
            string uiPerkName = StringConverter.GetUiPerkName(perkName.ToString());
            _buffText.text = $"{uiPerkName} ({duration})";
        }

        public void Clear()
        {
            _buffText.text = "";
            _name = PerkName.Empty;
        }
    }
}
