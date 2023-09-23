using TurnBasedUnits.Helpers;
using UnityEngine;

namespace TurnBasedUnits.UI
{
    public class UiScreen : MonoBehaviour
    {
        [SerializeField] private RoundsCounter _roundsCounter;
        [SerializeField] private CharacterUiPlate[] _characterUiPlates;

        public void Init(int[] characterIDs)
        {
            _roundsCounter.Init();

            for (int i = 0; i < _characterUiPlates.Length && i < characterIDs.Length; i++)
                _characterUiPlates[i].Init(characterIDs[i]);
        }

        public void OnTurnEnded(int nextCharacterID)
        {

        }

        public void OnRoundEnded(int newValue)
        {
            _roundsCounter.UpdateUi(newValue);
        }

        public void OnStatChanged(int newValue, StatType type, int id)
        {
            CharacterUiPlate characterUiPlate = GetCharacterUiPlate(id);

            if (characterUiPlate != null)
                characterUiPlate.OnStatChanged(newValue, type);
        }

        private CharacterUiPlate GetCharacterUiPlate(int id)
        {
            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
            {
                if (characterUiPlate.ID == id)
                    return characterUiPlate;
            }

            return null;
        }
    }
}
