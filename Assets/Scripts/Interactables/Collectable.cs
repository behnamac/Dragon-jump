using Controllers;
using Interfaces;
using UnityEngine;

namespace Interactables
{
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private int levelToGain = 1; 
        [SerializeField] private int coinToGain = 1; 
        
        public void InteractWithTrigger(PlayerController playerController)
        {
            playerController.PlayerLevelController.AddLevel(levelToGain);
            UiController.Instance.AddCoin(coinToGain);
            gameObject.SetActive(false);
        }

        public void InteractWithCollision(PlayerController playerController)
        {
            
        }
    }
}