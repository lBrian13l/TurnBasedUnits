using UnityEngine;
using TurnBasedUnits.Characters;

namespace TurnBasedUnits.Data
{
    [CreateAssetMenu(fileName = "NewDefaultStats", menuName = "Default Stats", order = 52)]
    public class DefaultStats : ScriptableObject
    {
        [SerializeField] private DefaultStatValue[] _values;

        public DefaultStatValue[] Values => _values;
    }
}
