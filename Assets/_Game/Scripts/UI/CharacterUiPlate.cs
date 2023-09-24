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
        public int PerkSlotsCount => _perkSlots.Length;

        public void Init()
        {
            _attackButton.onClick.AddListener(OnAttackButtonClicked);
            _buffButton.onClick.AddListener(OnBuffButtonClicked);
            _statPlate.Init();
        }

        public void UpdatePerkSlot(int index, string name, int value)
        {

        }

        public void OnStatChanged(StatType type, int newValue)
        {
            _statPlate.UpdateBar(type, newValue);
        }

        private void OnAttackButtonClicked()
        {
            AttackButtonClicked?.Invoke();
        }

        private void OnBuffButtonClicked()
        {
            BuffButtonClicked?.Invoke();
        }
    }
}
