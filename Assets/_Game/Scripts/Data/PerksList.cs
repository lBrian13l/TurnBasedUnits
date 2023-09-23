using UnityEngine;
using TurnBasedUnits.Characters;

namespace TurnBasedUnits.Data
{
    [CreateAssetMenu(fileName = "NewPerksList", menuName = "Perks List", order = 51)]
    public class PerksList : ScriptableObject
    {
        [SerializeField] private Perk[] _perks;

        public Perk[] Perks => _perks;
    }
}
