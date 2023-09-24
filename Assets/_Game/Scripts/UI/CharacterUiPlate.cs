using System;
using TurnBasedUnits.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedUnits.UI
{
    public class CharacterUiPlate : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _buffButton;
        [SerializeField] private UiPerkSlot[] _perkSlots;
        [SerializeField] private StatPlate _statPlate;

        private int _id;

        public event Action<int> AttackButtonClicked;
        public event Action<int> BuffButtonClicked;

        public int PerkSlotsCount => _perkSlots.Length;
        public int ID => _id;

        public void Init(int id)
        {
            _attackButton.onClick.AddListener(OnAttackButtonClicked);
            _buffButton.onClick.AddListener(OnBuffButtonClicked);
            _statPlate.Init();
            _id = id;
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
            AttackButtonClicked?.Invoke(ID);
        }

        private void OnBuffButtonClicked()
        {
            BuffButtonClicked?.Invoke(ID);
        }
    }
}
