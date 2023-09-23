using UnityEngine;
using System;
using System.Collections.Generic;
using TurnBasedUnits.Data;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.Characters
{
    public class Character : MonoBehaviour
    {
        private CharacterStats _stats;
        private List<Perk> _perks = new List<Perk>();

        public event Action Died;

        public int Health
        {
            get { return _stats.GetStat(StatType.Health); }
            private set
            {
                _stats.SetStat(StatType.Health, value);

                if (Health <= 0)
                    Died?.Invoke();
            }
        }

        public int Armor { get { return _stats.GetStat(StatType.Armor); } private set { _stats.SetStat(StatType.Armor, value); } }
        public int Vampirism { get { return _stats.GetStat(StatType.Vampirism); } private set { _stats.SetStat(StatType.Vampirism, value); } }
        public int AttackPower { get { return _stats.GetStat(StatType.AttackPower); } private set { _stats.SetStat(StatType.AttackPower, value); } }
        public int PerksCount => _perks.Count;

        public void Init(DefaultStats defaultStats)
        {
            _stats = new CharacterStats(defaultStats);
            _perks.Clear();
        }

        public void AddPerk(Perk perk)
        {
            _perks.Add(perk);

            foreach (PerkEffect effect in perk.Effects)
            {
                //switch (effect.Stat)
                //{
                //    case CharacterStat.Health:
                //        break;
                //    case CharacterStat.Armor:
                //        break;
                //    case CharacterStat.Vampirism:
                //        break;
                //    case CharacterStat.AttackPower:
                //        break;
                //}
            }
        }

        public void RemovePerk(Perk perk)
        {
            if (_perks.Contains(perk))
            {

            }
        }

        public void Attack(Character target)
        {
            int damage = AttackPower - target.Armor;

            if (damage <= 0)
                return;

            TryToLeach(damage, target.Health);
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        private void TryToLeach(int damage, int targetHealth)
        {
            if (Vampirism <= 0)
                return;

            int damageDone = Math.Min(damage, targetHealth);
            Health += Math.Min(damageDone, Vampirism);
        }
    }
}
