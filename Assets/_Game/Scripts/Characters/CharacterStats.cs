using System.Collections.Generic;
using TurnBasedUnits.Helpers;
using TurnBasedUnits.Data;
using System;

namespace TurnBasedUnits.Characters
{
    public class CharacterStats
    {
        private Dictionary<StatType, int> _stats;
        private DefaultStats _defaultStats;

        public CharacterStats(DefaultStats defaultStats)
        {
            for (int i = 0; i < defaultStats.Values.Length; i++)
            {
                DefaultStatValues defaultValues = defaultStats.Values[i];

                if (_stats.ContainsKey(defaultValues.Type))
                    continue;

                _stats.Add(defaultValues.Type, defaultValues.Start);
            }

            _defaultStats = defaultStats;
        }

        public int GetStat(StatType type)
        {
            if (_stats.ContainsKey(type))
                return _stats[type];

            throw new Exception($"Character stats don't contain {type}");
        }

        public void SetStat(StatType type, int value)
        {
            if (_stats.ContainsKey(type))
            {
                int min = GetMinValue(type);
                int max = GetMaxValue(type);
                _stats[type] = Math.Clamp(value, min, max);
                return;
            }

            throw new Exception($"Character stats don't contain {type}");
        }

        //public int GetViableValue(StatType type, int effectValue)
        //{
        //    int currentStatValue = GetStat(type);
        //    int changedValue = currentStatValue + effectValue;
        //    int viableValue = 
        //}

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
