using UnityEngine;
using System;
using TurnBasedUnits.Helpers;

namespace TurnBasedUnits.Characters
{
    public enum PerkType
    {
        Defensive,
        Offensive
    }

    [Serializable]
    public struct PerkEffect
    {
        [SerializeField] private StatType _stat;
        [SerializeField] private int _value;

        public PerkEffect(StatType stat, int value)
        {
            _stat = stat;
            _value = value;
        }

        public StatType Stat => _stat;
        public int Value => _value;
    }

    [Serializable]
    public class Perk : ICloneable
    {
        [SerializeField] private PerkName _name;
        [SerializeField] private PerkType _type;
        [SerializeField] private PerkEffect[] _effects;

        private int _duration;

        public event Action<PerkName, int> DurationChanged;
        public event Action<Perk> DurationEnded;

        public PerkName Name => _name;
        public PerkType Type => _type;
        public PerkEffect[] Effects => _effects;

        public void SetDuration(int duration)
        {
            _duration = duration;
            DurationChanged?.Invoke(_name, _duration);
        }

        public void DecreaseDuration()
        {
            _duration--;
            DurationChanged?.Invoke(_name, _duration);

            if (_duration <= 0)
                DurationEnded?.Invoke(this);
        }

        public object Clone() => MemberwiseClone();
    }
}
