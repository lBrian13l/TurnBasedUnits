using UnityEngine;
using System;
using TurnBasedUnits.Helpers;
using System.Collections.Generic;
using DG.Tweening;

namespace TurnBasedUnits.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterType _type;
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private CharacterPerks _perks;
        [SerializeField] MeshRenderer _renderer;
        [SerializeField] private Character _target;

        public event Action<CharacterType> Attacked;
        public event Action Died;

        public CharacterType Type => _type;
        public CharacterStats Stats => _stats;
        public CharacterPerks Perks => _perks;
        public int Health => _stats.GetStat(StatType.Health);
        public int Armor => _stats.GetStat(StatType.Armor);
        public int Vampirism => _stats.GetStat(StatType.Vampirism);
        public int AttackPower => _stats.GetStat(StatType.AttackPower);

        public void Init()
        {
            _stats.HealthWasted += OnHealthWasted;
            _perks.PerkAdded += _stats.AddBuff;
            _perks.PerkRemoved += _stats.RemoveBuff;
        }

        public void Restart()
        {
            _stats.Restart();
            _perks.Restart();
            _renderer.sharedMaterial.color = Color.white;
        }

        public void OnRoundEnded()
        {
            _perks.OnRoundEnded();
        }

        public void Attack()
        {
            int damage = (int)Mathf.Round(AttackPower / 100f * (100f - _target.Armor));
            TryToLeech(Math.Min(damage, _target.Health));
            List<Perk> offensivePerks = _perks.GetOffensivePerks();
            _target.TakeDamage(damage, offensivePerks);
            Attacked?.Invoke(_type);
        }

        public void TakeDamage(int damage, List<Perk> offensivePerks)
        {
            foreach (Perk perk in offensivePerks)
                _stats.ApplyDebuff(perk);

            if (damage <= 0)
                return;

            _stats.ChangeStat(StatType.Health, Health - damage);
            PlayDamageEffect();
        }

        private void PlayDamageEffect()
        {
            Material material = _renderer.sharedMaterial;
            material.DOColor(Color.red, 0.5f).OnComplete(() => material.DOColor(Color.white, 0.5f));
        }

        private void TryToLeech(int availableHealth)
        {
            if (Vampirism <= 0)
                return;

            int leechedHealth = (int)Mathf.Round(availableHealth / 100f * Vampirism);
            _stats.ChangeStat(StatType.Health, Health + leechedHealth);
        }

        private void OnHealthWasted() => Died?.Invoke();
    }
}
