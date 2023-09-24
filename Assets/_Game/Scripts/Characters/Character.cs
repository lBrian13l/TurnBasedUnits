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
        private List<Perk> _appliedPerks = new List<Perk>();
        private int _id;

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
        public int PerksCount => _appliedPerks.Count;
        public List<Perk> Perks => _appliedPerks;
        public int ID => _id;

        public void Init(DefaultStats defaultStats, int id)
        {
            //_stats = new CharacterStats(defaultStats);
            _appliedPerks.Clear();
            _id = id;
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

                for (int i = 0; i < _appliedPerks.Count; i++)
                {
                    if (_appliedPerks[i].ID == randomPerk.ID)
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

            _appliedPerks.Add((Perk)perk.Clone());
            perk.SetDuration(Random.Range(1, 4));
            perk.DurationEnded += RemovePerk;

            if (perk.Type == PerkType.Defensive)
                ApplyPerkEffects(perk);
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

        public void RemovePerk(Perk perk)
        {
            if (_appliedPerks.Contains(perk))
            {
                _appliedPerks.Remove(perk);
                perk.DurationEnded -= RemovePerk;
                RemovePerkEffects(perk);
            }
        }

        private void ApplyPerkEffects(Perk perk)
        {
            for (int i = 0; i < perk.Effects.Length; i++)
            {
                PerkEffect effect = perk.Effects[i];
                int viableValue = _stats.GetViableValue(effect.Stat, effect.Value);
                
                if (effect.Value != viableValue)
                {
                    effect = new PerkEffect(effect.Stat, viableValue);
                    perk.Effects[i] = effect;
                }

                switch (effect.Stat)
                {
                    case StatType.Health:
                        Health += effect.Value;
                        break;
                    case StatType.Armor:
                        Armor += effect.Value;
                        break;
                    case StatType.Vampirism:
                        Vampirism += effect.Value;
                        break;
                    case StatType.AttackPower:
                        AttackPower += effect.Value;
                        break;
                }
            }
        }

        private void RemovePerkEffects(Perk perk)
        {
            for (int i = 0; i < perk.Effects.Length; i++)
            {
                PerkEffect effect = perk.Effects[i];
                int viableValue = _stats.GetViableValue(effect.Stat, effect.Value);

                if (effect.Value != viableValue)
                    effect = new PerkEffect(effect.Stat, viableValue);

                switch (effect.Stat)
                {
                    case StatType.Health:
                        Health -= effect.Value;
                        break;
                    case StatType.Armor:
                        Armor -= effect.Value;
                        break;
                    case StatType.Vampirism:
                        Vampirism -= effect.Value;
                        break;
                    case StatType.AttackPower:
                        AttackPower -= effect.Value;
                        break;
                }
            }
        }
    }
}