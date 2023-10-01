using UnityEngine;
using TurnBasedUnits.Helpers;
using TurnBasedUnits.Data;
using System;

namespace TurnBasedUnits.UI
{
    public class StatPlate : MonoBehaviour
    {
        [SerializeField] private DefaultStats _defaultStats;
        [SerializeField] private Bar[] _bars;

        public void Init()
        {
            foreach (DefaultStatValues defaultValues in _defaultStats.Values)
            {
                StatType type = defaultValues.Type;

                if (type == StatType.AttackPower)
                    continue;

                GetBar(type).Init(defaultValues.Max);
            }
        }

        public void UpdateBar(StatType type, int newValue) => GetBar(type).UpdateUi(newValue);

        private Bar GetBar(StatType type)
        {
            foreach (Bar bar in _bars)
            {
                if (bar.Type == type)
                    return bar;
            }

            throw new Exception($"Couldn't find {type} bar");
        }
    }
}
