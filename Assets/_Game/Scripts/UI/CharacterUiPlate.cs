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
        [SerializeField] private StatPlate _statPlate;

        private int _id;

        public event Action<int> AttackButtonClicked;
        public event Action<int> BuffButtonClicked;

        public int ID => _id;

        public void Init(int id)
        {
            _attackButton.onClick.AddListener(OnAttackButtonClicked);
            _buffButton.onClick.AddListener(OnBuffButtonClicked);
            _statPlate.Init();
            _id = id;
        }

        public void OnStatChanged(int newValue, StatType type)
        {
            _statPlate.UpdateBar(newValue, type);
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
