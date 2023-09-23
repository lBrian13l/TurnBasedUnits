using System.Collections.Generic;
using TurnBasedUnits.Helpers;
using TurnBasedUnits.Data;
using System;

namespace TurnBasedUnits.Characters
{
    public class CharacterStats
    {
        private Dictionary<StatType, int> _stats;

        public CharacterStats(DefaultStats defaultStats)
        {
            for (int i = 0; i < defaultStats.Values.Length; i++)
            {
                DefaultStatValue defaultValue = defaultStats.Values[i];

                if (_stats.ContainsKey(defaultValue.Type))
                    continue;

                _stats.Add(defaultValue.Type, defaultValue.Start);
            }
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
                _stats[type] = value;
                return;
            }

            throw new Exception($"Character stats don't contain {type}");
        }
    }
}
