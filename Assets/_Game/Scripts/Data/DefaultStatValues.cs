using TurnBasedUnits.Helpers;
using System;
using UnityEngine;

namespace TurnBasedUnits.Data
{
    [Serializable]
    public struct DefaultStatValues
    {
        [SerializeField] private StatType _type;
        [SerializeField] private int _start;
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public StatType Type => _type;
        public int Start => _start;
        public int Min => _min;
        public int Max => _max;
    }
}
