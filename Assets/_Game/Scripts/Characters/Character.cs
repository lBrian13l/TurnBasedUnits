using UnityEngine;
using System;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType _type;
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private CharacterPerks _perks;
        [SerializeField] private Character _target;

        public event Action Died;

        public CharacterType Type => _type;
        public int Health => _stats.GetStat(StatType.Health);
        public int Armor => _stats.GetStat(StatType.Armor);
        public int Vampirism => _stats.GetStat(StatType.Vampirism);
        public int AttackPower => _stats.GetStat(StatType.AttackPower);

        public void Init()
        {
            _stats.Init();
            _stats.HealthWasted += OnHealthWasted;
        }

        public void Attack(Character target)
        {
            int damage = AttackPower - target.Armor;

            if (damage <= 0)
                return;

            TryToLeach(Math.Min(damage, target.Health));
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            _stats.ChangeStat(StatType.Health, -damage);
        }

        private void TryToLeach(int availableHealth)
        {
            if (Vampirism <= 0)
                return;

            _stats.ChangeStat(StatType.Health, Math.Min(availableHealth, Vampirism));
        }

        private void OnHealthWasted() => Died?.Invoke();
    }
}
