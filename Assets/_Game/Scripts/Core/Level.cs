using UnityEngine;
using TurnBasedUnits.Characters;
using TurnBasedUnits.UI;

namespace TurnBasedUnits.Core
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private UiScreen _uiScreen;
        [SerializeField] private Character[] _characters;

        private Character _controlledCharacter;

        void Start()
        {
            Restart();
        }

        private void Init()
        {
            foreach (Character character in _characters)
            {
                CharacterUiPlate characterUiPlate = _uiScreen.GetCharacterUiPlate(character.Type);
                
            }
        }

        private void Restart()
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                _characters[i].Init();
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
