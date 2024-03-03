using System;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        public static event Action OnTap;

        [SerializeField] private float delayBetweenTaps = 0.1f;

        private float _delayTimer;

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing) return;
            
            _delayTimer += delayBetweenTaps;
            
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (_delayTimer > delayBetweenTaps)
                {
                    _delayTimer = 0;
                    OnTap?.Invoke();
                }
            }
        }
    }
}