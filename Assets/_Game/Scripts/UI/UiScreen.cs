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

        //public int GetMaxPerkSlots()
        //{
        //    foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
        //    {
        //        if (characterUiPlate.PerkSlotsCount > maxSlots)
        //            maxSlots = characterUiPlate.PerkSlotsCount;
        //    }

        //    return maxSlots;
        //}

        //public void UpdatePerks(int id, string name, int turnsCount)
        //{
        //    for (int <)
        //    {
        //        if (id == characterUiPlate.ID)
        //            characterUiPlate.upda
        //    }
        //}

        public void OnStatChanged(int id, StatType type, int newValue)
        {
            CharacterUiPlate characterUiPlate = GetCharacterUiPlate(id);

            if (characterUiPlate != null)
                characterUiPlate.OnStatChanged(type, newValue);
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
