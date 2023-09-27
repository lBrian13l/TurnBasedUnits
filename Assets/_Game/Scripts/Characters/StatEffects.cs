using TurnBasedUnits.Helpers;
using UnityEngine;

namespace TurnBasedUnits.Characters
{
    public class StatEffects
    {
        private StatType _type;
        private int _positiveEffect;
        private int _negativeEffect;

        public StatEffects(StatType type, int startValue)
        {
            _type = type;
            _positiveEffect = startValue;
        }

        public StatType Type => _type;

        public int GetUpdatedValue()
        {
            return _positiveEffect + _negativeEffect;
        }

        public void ApplyBuff(int effect)
        {
            if (effect > 0)
                _positiveEffect += effect;
            else
                _negativeEffect += effect;
        }

        public void RemoveBuff(int effect)
        {
            if (effect > 0)
                _positiveEffect = Mathf.Max(_positiveEffect - effect, 0);
            else
                _negativeEffect = Mathf.Min(_negativeEffect - effect, 0);
        }

        public void ApplyDebuff(int effect)
        {
            _positiveEffect = Mathf.Max(_positiveEffect + effect, 0);
        }
    }
}
