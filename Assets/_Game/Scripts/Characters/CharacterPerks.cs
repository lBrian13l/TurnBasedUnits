using System.Collections.Generic;
using UnityEngine;
using TurnBasedUnits.Data;
using System;
using TurnBasedUnits.Helpers;
using Random = UnityEngine.Random;

namespace TurnBasedUnits.Characters
{
    public class CharacterPerks : MonoBehaviour
    {
        [SerializeField] private PerksList _perksList;

        private List<Perk> _appliedPerks = new List<Perk>();

        public event Action<Perk> PerkAdded;
        public event Action<Perk> PerkRemoved;
        public event Action<PerkName, int> PerkDurationChanged;

        public void Restart()
        {
            foreach (Perk perk in _appliedPerks)
            {
                perk.DurationChanged -= OnDurationChanged;
                perk.DurationEnded -= RemovePerk;
            }

            _appliedPerks.Clear();
        }

        public void OnRoundEnded()
        {
            for (int i = _appliedPerks.Count - 1; i >= 0; i--)
                _appliedPerks[i].DecreaseDuration();
        }

        public void AddRandomPerk()
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
                    if (_appliedPerks[i].Name == randomPerk.Name)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);

            AddPerk(randomPerk);
        }

        public List<Perk> GetOffensivePerks()
        {
            List<Perk> offensivePerks = new List<Perk>();

            foreach (Perk perk in _appliedPerks)
            {
                if (perk.Type == PerkType.Offensive)
                    offensivePerks.Add(perk);
            }

            return offensivePerks;
        }

        private void OnDurationChanged(PerkName name, int duration) => PerkDurationChanged?.Invoke(name, duration);

        private void AddPerk(Perk perk)
        {
            if (perk == null)
                return;

            perk = (Perk)perk.Clone();
            _appliedPerks.Add(perk);
            perk.DurationChanged += OnDurationChanged;
            perk.DurationEnded += RemovePerk;
            perk.SetDuration(Random.Range(1, 4));

            if (perk.Type == PerkType.Defensive)
                PerkAdded?.Invoke(perk);
        }

        private void RemovePerk(Perk perk)
        {
            if (_appliedPerks.Contains(perk))
            {
                _appliedPerks.Remove(perk);
                perk.DurationChanged -= OnDurationChanged;
                perk.DurationEnded -= RemovePerk;

                if (perk.Type == PerkType.Defensive)
                    PerkRemoved?.Invoke(perk);
            }
        }
    }
}
