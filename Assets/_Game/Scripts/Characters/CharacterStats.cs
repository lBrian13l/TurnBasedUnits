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

        private Dictionary<StatType, int> _stats = new Dictionary<StatType, int>();
        private List<StatEffects> _statEffects = new List<StatEffects>();

        public event Action<StatType, int> StatChanged;
        public event Action HealthWasted;

        public void Restart()
        {
            _stats.Clear();
            _statEffects.Clear();

            foreach (DefaultStatValues defaultValues in _defaultStats.Values)
            {
                StatType type = defaultValues.Type;
                int startValue = defaultValues.Start;

                if (_stats.ContainsKey(type))
                    continue;

                _stats.Add(type, startValue);

                if (type != StatType.AttackPower)
                    StatChanged?.Invoke(type, startValue);

                if (type != StatType.Health)
                {
                    StatEffects effects = new StatEffects(type, startValue);
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
                //int newValue = _stats[type] + value;
                value = Clamp(type, value);
                _stats[type] = value;

                if (type != StatType.AttackPower)
                    StatChanged?.Invoke(type, value);
                if (type == StatType.Health && value <= 0)
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
                ChangeStat(perkEffect.Stat, statEffects.UpdatedValue);
            }
        }

        public void RemoveBuff(Perk perk)
        {
            foreach (PerkEffect perkEffect in perk.Effects)
            {
                StatEffects statEffects = GetEffects(perkEffect.Stat);
                statEffects.RemoveBuff(perkEffect.Value);
                ChangeStat(perkEffect.Stat, statEffects.UpdatedValue);
            }
        }

        public void ApplyDebuff(Perk perk)
        {
            foreach (PerkEffect perkEffect in perk.Effects)
            {
                StatEffects statEffects = GetEffects(perkEffect.Stat);
                statEffects.ApplyDebuff(perkEffect.Value);
                ChangeStat(perkEffect.Stat, statEffects.UpdatedValue);
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
