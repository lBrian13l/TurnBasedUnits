using UnityEngine;
using System;
using System.Collections.Generic;
using TurnBasedUnits.Data;
using TurnBasedUnits.Helpers;
using Random = UnityEngine.Random;

namespace TurnBasedUnits.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private PerksList _perksList;

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

        public void AddRandomPerk()
        {
            Perk[] perks = _perksList.Perks;
            Perk randomPerk;
            bool contains;

            do
            {
                contains = false;
                randomPerk = perks[Random.Range(0, perks.Length)];

                for (int i = 0; i < _perks.Count; i++)
                {
                    if (_perks[i].ID == randomPerk.ID)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);

            AddPerk(randomPerk);
        }

        public void AddPerk(Perk perk)
        {
            if (perk == null)
                return;

            _perks.Add(perk);
            perk.SetDuration(Random.Range(1, 4));

            if (perk.Type == PerkType.Passive)
                ApplyPerkEffects(perk);
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

            TryToLeach(Math.Min(damage, target.Health));
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        private void TryToLeach(int availableHealth)
        {
            if (Vampirism <= 0)
                return;

            Health += Math.Min(availableHealth, Vampirism);
        }

        private void ApplyPerkEffects(Perk perk)
        {
            for (int i = 0; i < perk.Effects.Length; i++)
            {

            }
        }
    }
}
