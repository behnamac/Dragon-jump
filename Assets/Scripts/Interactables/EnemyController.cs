using Characters;
using Controllers;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Interactables
{
    public class EnemyController : CharacterLevelBase, IInteractable
    {
        [SerializeField] private GameObject destroyParticel;
        [SerializeField] private Renderer meshForVisible;
        [SerializeField] private Color goodColor = Color.green;
        [SerializeField] private Color badColor = Color.red;

        private Animator[] _animators;

        private void Awake()
        {
            _animators = GetComponentsInChildren<Animator>();

            PlayerLevelController.OnAddLevel += UpdateLevelImage;
        }
        protected override void Start()
        {
            base.Start();

         

            if (ChangeEnemyLevel.Instance != null) 
            {
                ChangeEnemyLevel.Instance.AddEnemy(this);
            }

            UpdateLevelImage();
        }
        private void Update()
        {
        }
        private void OnDestroy()
        {
            PlayerLevelController.OnAddLevel -= UpdateLevelImage;
        }
        public void InteractWithTrigger(PlayerController playerController)
        {
            int playerLevel = playerController.PlayerLevelController.CurrentLevel;

            if (playerLevel >= currentLevel)
            {
                gameObject.SetActive(false);
                SpawnParticel();
                playerController.PlayerLevelController.AddLevel(currentLevel);
                playerController.PlayerAnimationController.ActivePunchAnimation();
            }
            else
            {
                playerController.PlayerLevelController.RemoveLevel(1);

                // start victory anim
                currentLevel += 1;
                UpdateLevelText();
            }

            //Invoke(nameof(ActiveTrigger), 1);
        }

        public void InteractWithCollision(PlayerController playerController)
        {
            
        }

        void SpawnParticel() 
        {
            var particel = Instantiate(destroyParticel, transform.position, Quaternion.identity);
            Destroy(particel, 2);
        }

        public void SetNewLevel(int value)
        {
            /* if (!meshForVisible.isVisible)
             {*/
            currentLevel = value;
            GetComponent<EnemyLevelController>().SetNewItem();
            UpdateLevelText();
            UpdateLevelImage();
            // }
        }

        void UpdateLevelImage() 
        {
            if (currentLevel <= PlayerLevelController.Instance.CurrentLevel)
            {
                levelText.color = goodColor;
            }
            else
            {
                levelText.color = badColor;

            }

        }

        public int GetCurrentLevel() 
        {
            return currentLevel;
        }
    }
}