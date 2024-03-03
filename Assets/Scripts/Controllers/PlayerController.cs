using Player;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerFlipController PlayerFlipController { get; private set; }
        public PlayerLevelController PlayerLevelController { get; private set; }
        public PlayerAnimationController PlayerAnimationController { get; private set; }

        private void Awake()
        {
            PlayerFlipController = GetComponent<PlayerFlipController>();
            PlayerLevelController = GetComponent<PlayerLevelController>();
            PlayerAnimationController = GetComponent<PlayerAnimationController>();
        }
    }
}