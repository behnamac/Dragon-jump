using UnityEngine;

namespace AnimationStateMachines
{
    public class PlayerFlipBehaviour : StateMachineBehaviour
    {
        private static readonly int LandTrigger = Animator.StringToHash("LandTrigger");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(LandTrigger);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // animator.SetBool("isFlipping", false);
            // animator.SetTrigger(FallTrigger);
        }
    }
}
