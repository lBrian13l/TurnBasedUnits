using UnityEngine;
using TurnBasedUnits.Characters;
using TurnBasedUnits.UI;
using TurnBasedUnits.Helpers;
using System;

namespace TurnBasedUnits.Core
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private UiScreen _uiScreen;
        [SerializeField] private Character[] _characters;

        private int _roundsCount;
        private bool _gameFinished;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            _uiScreen.Init();
            _uiScreen.RestartButtonClicked += Restart;

            foreach (Character character in _characters)
            {
                CharacterUiPlate characterUiPlate = _uiScreen.GetCharacterUiPlate(character.Type);
                character.Died += OnCharacterDied;
                character.Attacked += EndTurn;
                character.Stats.StatChanged += characterUiPlate.OnStatChanged;
                character.Perks.PerkDurationChanged += characterUiPlate.OnPerkDurationChanged;
                characterUiPlate.AttackButtonClicked += character.Attack;
                characterUiPlate.BuffButtonClicked += character.Perks.AddRandomPerk;
                character.Init();
            }

            Restart();
        }

        private void Restart()
        {
            foreach (Character character in _characters)
                character.Restart();

            _gameFinished = false;
            _roundsCount = 1;
            _uiScreen.Restart(_roundsCount);
        }

        private void EndTurn(CharacterType currentCharacter)
        {
            if (_gameFinished == false)
                _uiScreen.ActivateCharacterPlate(GetNextCharacter(currentCharacter));
        }

        private CharacterType GetNextCharacter(CharacterType currentCharacter)
        {
            CharacterType nextCharacter = (CharacterType)(int)++currentCharacter;

            if (Enum.IsDefined(typeof(CharacterType), nextCharacter))
            {
                return nextCharacter;
            }
            else
            {
                EndRound();
                return CharacterType.First;
            }
        }

        private void EndRound()
        {
            foreach (Character character in _characters)
                character.OnRoundEnded();

            _roundsCount++;
            _uiScreen.OnRoundEnded(_roundsCount);
        }

        private void OnCharacterDied()
        {
            _gameFinished = true;
            _uiScreen.DeactivateCharacterPlates();
        }
    }
}
