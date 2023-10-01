using System;
using TurnBasedUnits.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedUnits.UI
{
    public class CharacterUiPlate : MonoBehaviour
    {
        [SerializeField] private CharacterType _type;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _buffButton;
        [SerializeField] private UiPerkSlot[] _perkSlots;
        [SerializeField] private StatPlate _statPlate;

        public event Action AttackButtonClicked;
        public event Action BuffButtonClicked;

        public CharacterType Type => _type;

        public void Init()
        {
            _attackButton.onClick.AddListener(() => AttackButtonClicked?.Invoke());
            _buffButton.onClick.AddListener(OnBuffButtonClicked);
            _statPlate.Init();
        }

        public void Restart()
        {
            foreach (UiPerkSlot uiPerkSlot in _perkSlots)
                uiPerkSlot.Restart();
        }

        public void Activate()
        {
            _attackButton.gameObject.SetActive(true);
            _buffButton.gameObject.SetActive(GetEmptyPerkSlot() != null);
        }

        public void Deactivate()
        {
            _attackButton.gameObject.SetActive(false);
            _buffButton.gameObject.SetActive(false);
        }

        public void OnStatChanged(StatType type, int newValue) => _statPlate.UpdateBar(type, newValue);

        public void OnPerkDurationChanged(PerkName perkName, int duration)
        {
            UiPerkSlot perkSlot = GetPerkSlot(perkName);

            if (perkSlot == null)
                throw new Exception("No available perk slot");

            if (duration == 0)
                perkSlot.Clear();
            else
                perkSlot.UpdateSlot(perkName, duration);
        }

        private UiPerkSlot GetPerkSlot(PerkName name)
        {
            foreach (UiPerkSlot perkSlot in _perkSlots)
            {
                if (perkSlot.Name == name)
                    return perkSlot;
            }

            return GetEmptyPerkSlot();
        }

        private UiPerkSlot GetEmptyPerkSlot()
        {
            foreach (UiPerkSlot perkSlot in _perkSlots)
            {
                if (perkSlot.Name == PerkName.Empty)
                    return perkSlot;
            }

            return null;
        }

        private void OnBuffButtonClicked()
        {
            _buffButton.gameObject.SetActive(false);
            BuffButtonClicked?.Invoke();
        }
    }
}
