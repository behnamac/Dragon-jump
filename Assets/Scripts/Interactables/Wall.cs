using Controllers;
using Enums;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Wall : MonoBehaviour, IInteractable
    {
        [SerializeField] private int levelToRemove;

        public void InteractWithTrigger(PlayerController playerController)
        {
        }

        public void InteractWithCollision(PlayerController playerController)
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;
            
            playerController.PlayerFlipController.PushBack();
           // playerController.PlayerLevelController.RemoveLevel(levelToRemove);
        }
    }
}