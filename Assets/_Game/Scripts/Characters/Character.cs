using UnityEngine;
using System;
using TurnBasedUnits.Helpers;
using System.Collections.Generic;

namespace TurnBasedUnits.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType _type;
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private CharacterPerks _perks;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] private Character _target;

        public event Action Died;

        public CharacterType Type => _type;
        public CharacterStats Stats => _stats;
        public CharacterPerks Perks => _perks;
        public int Health => _stats.GetStat(StatType.Health);
        public int Armor => _stats.GetStat(StatType.Armor);
        public int Vampirism => _stats.GetStat(StatType.Vampirism);
        public int AttackPower => _stats.GetStat(StatType.AttackPower);

        public void Init()
        {
            _stats.HealthWasted += OnHealthWasted;
            _perks.PerkAdded += _stats.AddBuff;
            _perks.PerkRemoved += _stats.RemoveBuff;
        }

        public void Restart()
        {
            _stats.Init();
        }

        public void Attack(Character target)
        {
            int damage = (int)Mathf.Round(AttackPower / 100f * (100f - target.Armor));
            TryToLeech(Math.Min(damage, target.Health));
            List<Perk> offensivePerks = _perks.GetOffensivePerks();
            target.TakeDamage(damage, offensivePerks);
        }

        public void TakeDamage(int damage, List<Perk> offensivePerks)
        {
            _stats.ChangeStat(StatType.Health, Health - damage);

            foreach (Perk perk in offensivePerks)
            {
                _stats.ApplyDebuff(perk);
            }
        }

        private void PlayDamageEffect()
        {

        }

        private void TryToLeech(int availableHealth)
        {
            if (Vampirism <= 0)
                return;

            int leechedHealth = (int)Mathf.Round(availableHealth / 100 * Vampirism);
            _stats.ChangeStat(StatType.Health, Health + leechedHealth);
        }

        private void OnHealthWasted() => Died?.Invoke();
    }
}
