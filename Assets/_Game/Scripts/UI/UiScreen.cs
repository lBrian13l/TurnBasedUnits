using TurnBasedUnits.Helpers;
using UnityEngine;

namespace TurnBasedUnits.UI
{
    public class UiScreen : MonoBehaviour
    {
        [SerializeField] private RoundsCounter _roundsCounter;
        [SerializeField] private CharacterUiPlate[] _characterUiPlates;

        public void Init()
        {
            _roundsCounter.Init();

            for (int i = 0; i < _characterUiPlates.Length; i++)
                _characterUiPlates[i].Init();
        }

        public CharacterUiPlate GetCharacterUiPlate(CharacterType type)
        {
            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
            {
                if (characterUiPlate.Type == type)
                    return characterUiPlate;
            }

            return null;
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
    }
}
