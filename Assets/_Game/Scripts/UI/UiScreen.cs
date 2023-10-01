using TurnBasedUnits.Helpers;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace TurnBasedUnits.UI
{
    public class UiScreen : MonoBehaviour
    {
        [SerializeField] private RoundsCounter _roundsCounter;
        [SerializeField] private Button _restartButton;
        [SerializeField] private CharacterUiPlate[] _characterUiPlates;

        public event Action RestartButtonClicked;

        public void Init()
        {
            _restartButton.onClick.AddListener(() => RestartButtonClicked?.Invoke());

            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
                characterUiPlate.Init();
        }

        public void Restart(int roundsCount)
        {
            _roundsCounter.UpdateUi(roundsCount);

            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
                characterUiPlate.Restart();

            ActivateCharacterPlate(CharacterType.First);
        }

        public void ActivateCharacterPlate(CharacterType type)
        {
            DeactivateCharacterPlates();
            GetCharacterUiPlate(type).Activate();
        }

        public void DeactivateCharacterPlates()
        {
            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
                characterUiPlate.Deactivate();
        }

        public CharacterUiPlate GetCharacterUiPlate(CharacterType type)
        {
            foreach (CharacterUiPlate characterUiPlate in _characterUiPlates)
            {
                if (characterUiPlate.Type == type)
                    return characterUiPlate;
            }

            throw new Exception($"UI plates don't contain {type}");
        }

        public void OnRoundEnded(int roundsCount)
        {
            _roundsCounter.UpdateUi(roundsCount);
        }
    }
}
