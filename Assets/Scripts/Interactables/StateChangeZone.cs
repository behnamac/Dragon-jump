using Controllers;
using Enums;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class StateChangeZone : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameState stateToActivate = GameState.Lose;
        
        public void InteractWithTrigger(PlayerController playerController)
        {
            GameManager.Instance.ChangeGameState(stateToActivate);

            if (stateToActivate == GameState.RunToFinish) 
            {
                playerController.PlayerFlipController.Run();
                CameraController.Instance.ChangeCameraPos("PostLevel");
            }
        }

        public void InteractWithCollision(PlayerController playerController)
        {
        }
    }
}