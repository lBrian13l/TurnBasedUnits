using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedUnits.Data;

namespace TurnBasedUnits.Characters
{
    public class CharacterPerks : MonoBehaviour
    {
        [SerializeField] private PerksList _perksList;

        private List<Perk> _appliedPerks;

        public void Init()
        {
            _appliedPerks = new List<Perk>();
        }

        private void AddRandomPerk()
        {
            Perk[] perks = _perksList.Perks;
            Perk randomPerk;
            bool contains;

            do
            {
                contains = false;
                randomPerk = perks[Random.Range(0, perks.Length)];

                for (int i = 0; i < _appliedPerks.Count; i++)
                {
                    if (_appliedPerks[i].ID == randomPerk.ID)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);

            AddPerk(randomPerk);
        }

        private void AddPerk(Perk perk)
        {
            if (perk == null)
                return;

            _appliedPerks.Add((Perk)perk.Clone());
            perk.SetDuration(Random.Range(1, 4));
            perk.DurationEnded += RemovePerk;

            if (perk.Type == PerkType.Defensive)
                ApplyEffects(perk);
        }

        private void ApplyEffects(Perk perk)
        {
            for (int i = 0; i < perk.Effects.Length; i++)
            {
                PerkEffect effect = perk.Effects[i];
                //int viableValue = _stats.GetViableValue(effect.Stat, effect.Value);

                //if (effect.Value != viableValue)
                //{
                //    effect = new PerkEffect(effect.Stat, viableValue);
                //    perk.Effects[i] = effect;
                //}

                //_stats.ChangeStat(effect.Stat, effect.Value);
            }
        }

        private void RemovePerk(Perk perk)
        {
            if (_appliedPerks.Contains(perk))
            {
                _appliedPerks.Remove(perk);
                perk.DurationEnded -= RemovePerk;
                RemoveEffects(perk);
            }
        }

        private void RemoveEffects(Perk perk)
        {
            for (int i = 0; i < perk.Effects.Length; i++)
            {
                PerkEffect effect = perk.Effects[i];
                //int viableValue = _stats.GetViableValue(effect.Stat, -effect.Value);

                //if (effect.Value != viableValue)
                //    effect = new PerkEffect(effect.Stat, viableValue);

                //_stats.ChangeStat(effect.Stat, -effect.Value);
            }
        }
    }
}
