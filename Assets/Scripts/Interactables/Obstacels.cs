using Controllers;
using Enums;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Obstacels : MonoBehaviour, IInteractable
    {
        [SerializeField] private int levelToRemove = 1;

        public void InteractWithTrigger(PlayerController playerController)
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;

            playerController.PlayerFlipController.PushBack();
            playerController.PlayerLevelController.RemoveLevel(levelToRemove);
        }

        public void InteractWithCollision(PlayerController playerController)
        {
        }
    }
}
