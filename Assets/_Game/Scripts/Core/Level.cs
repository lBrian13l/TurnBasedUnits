using UnityEngine;
using TurnBasedUnits.Characters;
using TurnBasedUnits.UI;
using TurnBasedUnits.Data;

namespace TurnBasedUnits.Core
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private UiScreen _uiScreen;
        [SerializeField] private Character[] _characters;
        [SerializeField] private DefaultStats _defaultStats;

        private Character _controlledCharacter;

        void Start()
        {
            Restart();
        }

        private void Restart()
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                _characters[i].Init(_defaultStats, i);
            }

            if (_characters.Length > 0)
                SetControlledCharacter(_characters[0]);
        }

        private void SetControlledCharacter(Character character)
        {
            if (character != null)
                _controlledCharacter = character;
        }

        private void UpdatePerkSlots()
        {
            //foreach (Character character in _characters)
            //{
            //    int i = 0;
            //    int maxIndex = _uiScreen.GetMaxPerkSlots(character.ID);

            //    for (; i < maxIndex; i++)
            //    {
            //        if (i < _characters.Length)
            //            _uiScreen.UpdatePerks()
            //    }
            //}
        }
    }
}
