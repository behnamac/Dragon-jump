using Controllers;
using Enums;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Road : MonoBehaviour, IInteractable
    {
        public void InteractWithTrigger(PlayerController playerController)
        {
        }

        public void InteractWithCollision(PlayerController playerController)
        {
            GameState gameState = GameManager.Instance.CurrentGameState;
            bool canRun = gameState == GameState.Playing || gameState == GameState.RunToFinish;
            if (!canRun) return;
            
            playerController.PlayerFlipController.Run();
        }
    }
}