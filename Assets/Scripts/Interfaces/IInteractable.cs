using Controllers;

namespace Interfaces
{
    public interface IInteractable
    {
        void InteractWithTrigger(PlayerController playerController);
        void InteractWithCollision(PlayerController playerController);
    }
}