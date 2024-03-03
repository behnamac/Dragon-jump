using System;
using Enums;
using Levels;
using UnityEngine;

namespace Controllers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static event Action<GameState> OnStateChanged;
        
        public bool IsLevelFinished { get; private set; }
        public GameState CurrentGameState { get; private set; }

        private void Awake()
        {
            if (Instance == null || Instance != this)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            LevelManager.OnLevelLoad += ActivateStart;
            LevelManager.OnLevelStart += ActivatePlaying;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelLoad -= ActivateStart;
            LevelManager.OnLevelStart -= ActivatePlaying;
        }

        public void ChangeGameState(GameState stateToChange)
        {
            if (CurrentGameState == stateToChange) return;

            CurrentGameState = stateToChange;
            OnStateChanged?.Invoke(CurrentGameState);

            IsLevelFinished = CurrentGameState == GameState.Win || CurrentGameState == GameState.Lose;

            if (stateToChange == GameState.Win) 
            {
                LevelManager.Instance.LevelComplete();
            }
        }

        private void ActivateStart(Level levelData) => ChangeGameState(GameState.Start);
        private void ActivatePlaying(Level levelData) => ChangeGameState(GameState.Playing);
    }
}