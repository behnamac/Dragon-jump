using Controllers;
using Dreamteck.Splines;
using Levels;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SplineFollower))]
    public class PlayerSplineFollowControl : AbstractPlayerMoveController
    {
        #region SERIALIZE FIELDS

        [Header("FOLLOWER")]
        [SerializeField] private SplineFollower follower;

        #endregion

        #region PRIVATE FIELDS

        private bool _isMove;
        private bool _isHorizontalMoveLock;
        private float _mouseXStartPosition;
        private float _swipeDelta;

        #endregion

        #region PRIVATE METHODS

        private void SetSpline()
        {
            follower.spline = FindObjectOfType<SplineComputer>();
            follower.followSpeed = forwardSpeed;
            follower.follow = false;
        }

        #endregion

        #region PUBLIC METHODS

        public override void HorizontalMoveControl()
        {
            if (_isHorizontalMoveLock) return;

            // MOUSE DOWN
            if (Input.GetMouseButtonDown(0)) _mouseXStartPosition = Input.mousePosition.x;

            // MOUSE ON PRESS
            if (Input.GetMouseButton(0))
            {
                if (!playerAnimator.GetBool(moveParameterName)) playerAnimator.SetBool(moveParameterName, true);

                _swipeDelta = Input.mousePosition.x - _mouseXStartPosition;
                _mouseXStartPosition = Input.mousePosition.x;
            }

            // MOUSE UP
            if (Input.GetMouseButtonUp(0)) _swipeDelta = 0;

            follower.motion.offset = HorizontalPosition(follower.motion.offset, _swipeDelta);
        }

        public override void StartRun()
        {
            _isMove = true;
            follower.follow = true;
            playerAnimator.SetBool(moveParameterName, true);
        }

        public override void StopRun()
        {
            _isMove = false;
            follower.follow = false;
            playerAnimator.SetBool(moveParameterName, false);
        }

        public override void StopHorizontalControl(bool controlIsLock = true) => _isHorizontalMoveLock = controlIsLock;

        #endregion

        #region CUSTOM EVENT METHODS

        private void OnLevelLoad(Level levelData) => SetSpline();

        private void OnLevelStart(Level levelData) => StartRun();

        private void OnLevelFail(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            StopHorizontalControl();
        }

        private void OnLevelComplete(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        #endregion

        #region UNITY EVENT METHODS

        protected override void OnComponentAwake()
        {
            base.OnComponentAwake();
            LevelManager.OnLevelLoad += OnLevelLoad;
        }

        protected override void OnComponentStart()
        {
            base.OnComponentAwake();
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelComplete += OnLevelComplete;
        }

        protected override void OnComponentUpdate()
        {
            if (!_isMove) return;
            HorizontalMoveControl();
        }

        protected override void OnComponentDestroy()
        {
            LevelManager.OnLevelLoad -= OnLevelLoad;
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelComplete -= OnLevelComplete;
        }

        #endregion
    }
}