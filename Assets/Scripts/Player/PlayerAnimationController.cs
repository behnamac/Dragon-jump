using Controllers;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Animation Hash IDs

        private static readonly int IsFallingHashID = Animator.StringToHash("isFalling");
        private static readonly int IsRunningHashID = Animator.StringToHash("isRunning");
        private static readonly int IsFlippingHashID = Animator.StringToHash("isFlipping");
        private static readonly int LandTriggerHashID = Animator.StringToHash("LandTrigger");
        private static readonly int DanceTriggerHashID = Animator.StringToHash("DanceTrigger");
        private static readonly int DeathTriggerHashID = Animator.StringToHash("DeathTrigger");

        public int PunchNumber { get; private set; }
        #endregion

        [SerializeField] private Animator[] animators;

        private void Awake()
        {
            GameManager.OnStateChanged += CheckGameState;
        }

        private void OnDestroy()
        {
            GameManager.OnStateChanged -= CheckGameState;
        }

        public void ActivateFlipAnimation() => ActivateAnimation(IsFlippingHashID);
        public void ActivateFallAnimation() => ActivateAnimation(IsFallingHashID);
        public void ActivateRunAnimation() => ActivateAnimation(IsRunningHashID);

        public void ActivePunchAnimation() 
        {
            if (!animators[0].GetBool("isRunning")) return;
            for (int i = 0; i < animators.Length; i++)
            {
                int punchN = animators[i].GetInteger("PunchNumber");
                PunchNumber = punchN == 0 ? 1 : 0;
                animators[i].SetInteger("PunchNumber", PunchNumber);
                animators[i].SetTrigger("Punch");
            }
        }

        private void ActivateAnimation(int hashID)
        {
            for (int i = 0; i < animators.Length; i++)
            {

                animators[i].SetBool(IsFlippingHashID, false);
                animators[i].SetBool(IsFallingHashID, false);
                animators[i].SetBool(IsRunningHashID, false);

                animators[i].SetBool(hashID, true);
            }
        }
        
        private void TriggerAnimation(int hashID)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool(IsFlippingHashID, false);
                animators[i].SetBool(IsFallingHashID, false);
                animators[i].SetBool(IsRunningHashID, false);

                animators[i].SetTrigger(hashID);
            }
        }

        private void ActivateDanceAnimation() => TriggerAnimation(DanceTriggerHashID);
        private void ActivateDeathAnimation() => TriggerAnimation(DeathTriggerHashID);

        private void CheckGameState(GameState gameState)
        {
            if (gameState == GameState.Win)
            {
                ActivateDanceAnimation();
            }
            else if (gameState == GameState.Lose)
            {
                ActivateDeathAnimation();
            }
        }
    }
}