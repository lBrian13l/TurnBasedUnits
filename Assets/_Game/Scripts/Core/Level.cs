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
            foreach (Character character in _characters)
            {
                character.Init(_defaultStats);
            }

            if (_characters.Length > 0)
                SetControlledCharacter(_characters[0]);
        }

        private void SetControlledCharacter(Character character)
        {
            if (character != null)
                _controlledCharacter = character;
        }
    }
}
