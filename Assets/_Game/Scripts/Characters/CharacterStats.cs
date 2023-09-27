using System.Collections.Generic;
using TurnBasedUnits.Helpers;
using TurnBasedUnits.Data;
using System;
using UnityEngine;

namespace TurnBasedUnits.Characters
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private DefaultStats _defaultStats;

        private Dictionary<StatType, int> _stats;
        private List<StatEffects> _statEffects;

        public event Action<StatType, int> StatChanged;
        public event Action HealthWasted;

        public void Init()
        {
            _stats = new Dictionary<StatType, int>();
            _statEffects = new List<StatEffects>();

            for (int i = 0; i < _defaultStats.Values.Length; i++)
            {
                DefaultStatValues defaultValues = _defaultStats.Values[i];

                if (_stats.ContainsKey(defaultValues.Type))
                    continue;

                _stats.Add(defaultValues.Type, defaultValues.Start);

                if (defaultValues.Type != StatType.Health)
                {
                    StatEffects effects = new StatEffects(defaultValues.Type, defaultValues.Start);
                    _statEffects.Add(effects);
                }
            }
        }

        public int GetStat(StatType type)
        {
            if (_stats.ContainsKey(type))
                return _stats[type];

            throw new Exception($"Character stats don't contain {type}");
        }

        public void ChangeStat(StatType type, int value)
        {
            if (_stats.ContainsKey(type))
            {
                int newValue = _stats[type] + value;
                newValue = Clamp(type, newValue);
                _stats[type] = newValue;

                if (type != StatType.AttackPower)
                    StatChanged?.Invoke(type, newValue);
                if (type == StatType.Health && newValue <= 0)
                    HealthWasted?.Invoke();

                return;
            }

            throw new Exception($"Character stats don't contain {type}");
        }

        public void AddBuff(Perk perk)
        {
            foreach (PerkEffect perkEffect in perk.Effects)
            {
                StatEffects statEffects = GetEffects(perkEffect.Stat);
                statEffects.ApplyBuff(perkEffect.Value);
                ChangeStat(perkEffect.Stat, statEffects.GetUpdatedValue());
            }
        }

        public void RemoveBuff(Perk perk)
        {
            foreach (PerkEffect perkEffect in perk.Effects)
            {
                StatEffects statEffects = GetEffects(perkEffect.Stat);
                statEffects.RemoveBuff(perkEffect.Value);
                ChangeStat(perkEffect.Stat, statEffects.GetUpdatedValue());
            }
        }

        public void ApplyDebuff(Perk perk)
        {
            foreach (PerkEffect perkEffect in perk.Effects)
            {
                StatEffects statEffects = GetEffects(perkEffect.Stat);
                statEffects.ApplyDebuff(perkEffect.Value);
                ChangeStat(perkEffect.Stat, statEffects.GetUpdatedValue());
            }
        }

        private StatEffects GetEffects(StatType type)
        {
            foreach (StatEffects effects in _statEffects)
            {
                if (effects.Type == type)
                    return effects;
            }

            throw new Exception($"Stat effects don't contain {type}");
        }

        private int Clamp(StatType type, int value)
        {
            int min = GetMinValue(type);
            int max = GetMaxValue(type);
            return Math.Clamp(value, min, max);
        }

        private int GetMinValue(StatType type)
        {
            foreach (DefaultStatValues defaultValues in _defaultStats.Values)
            {
                if (defaultValues.Type == type)
                    return defaultValues.Min;
            }

            throw new Exception($"Default stats don't contain {type}");
        }

        private int GetMaxValue(StatType type)
        {
            foreach (DefaultStatValues defaultValues in _defaultStats.Values)
            {
                if (defaultValues.Type == type)
                    return defaultValues.Max;
            }

            throw new Exception($"Default stats don't contain {type}");
        }
    }
}
