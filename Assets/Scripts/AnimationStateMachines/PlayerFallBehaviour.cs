using Player;
using UnityEngine;

namespace AnimationStateMachines
{
    public class PlayerFallBehaviour : StateMachineBehaviour
    {
        private TrailRenderer[] _fallTrails;

        private void Awake()
        {
            Transform player = FindObjectOfType<PlayerCollisionControl>().transform;
            _fallTrails = player.GetComponentsInChildren<TrailRenderer>();
            
            SetFallTrailsEmittingState(false);
        }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SetFallTrailsEmittingState(true);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SetFallTrailsEmittingState(false);
        }
        
        private void SetFallTrailsEmittingState(bool status)
        {
            foreach (TrailRenderer trail in _fallTrails)
            {
                trail.emitting = status;
            }
        }
    }
}
