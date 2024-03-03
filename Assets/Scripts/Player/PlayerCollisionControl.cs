using Controllers;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionControl : MonoBehaviour
    {
        #region PRIVATE FIELDS

        private PlayerController _playerController;

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        // TRIGGER EVENTS
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.InteractWithTrigger(_playerController);
            }
        }

        private void OnTriggerExit(Collider other)
        {
        }


        // COLLISION EVENTS
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.InteractWithCollision(_playerController);
            }
        }

        private void OnCollisionExit(Collision other)
        {
        }

        #endregion
    }
}