using UnityEngine;

namespace TurnBasedUnits.Data
{
    [CreateAssetMenu(fileName = "NewDefaultStats", menuName = "Default Stats", order = 52)]
    public class DefaultStats : ScriptableObject
    {
        [SerializeField] private DefaultStatValues[] _values;

        public DefaultStatValues[] Values => _values;
    }
}
