using Characters;
using Controllers;
using Enums;
using Structs;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Player
{
    public class PlayerLevelController : CharacterLevelBase
    {
        public static PlayerLevelController Instance;

        [Header("Level Up")]
        [SerializeField] private ItemHolder[] allItems;
        [SerializeField] private LevelUpData[] levelUpDatas;
        
        public int CurrentLevel => currentLevel;
        public int ItemNumber { get; set; }
        private Vector3 _firstScale;
        private PlayerController _playerController;

        public static UnityAction OnAddLevel;

        private void Awake() 
        {
            Instance = this;
        }
        protected override void Start()
        {
            base.Start();
            _firstScale = transform.localScale;
            _playerController = GetComponent<PlayerController>();
        }
        public void AddLevel(int levelToGain)
        {
            currentLevel += levelToGain;
            UpdateLevelText();

            //ChangeMesh
            SetNewItem();
            OnAddLevel?.Invoke();

            // level up effect
            transform.DOScale(_firstScale * 1.2f, 0.2f).OnComplete(() =>
            {
                transform.DOScale(_firstScale, 0.2f);
            });
        }

        public bool RemoveLevel(int levelToLose)
        {
            currentLevel -= levelToLose;
            currentLevel = Mathf.Clamp(currentLevel, 0, currentLevel);
            UpdateLevelText();
            if (currentLevel <= 0)
            {
                GameManager.Instance.ChangeGameState(GameState.Lose);
                return false;
            }
            _playerController.PlayerFlipController.PushBack();

            // level up effect
            transform.DOScale(_firstScale * 1.2f, 0.2f).OnComplete(() =>
            {
                transform.DOScale(_firstScale, 0.2f);
            });
            return true;
        }

        private void SetNewItem()
        {
            for (int i = 0; i < allItems.Length; i++)
            {
                if (currentLevel >= allItems[i].targetLevel && !allItems[i].active)
                {
                    for (int j = 0; j < allItems[i].diactiveItems.Length; j++)
                    {
                        allItems[i].diactiveItems[j].SetActive(false);
                    }
                    for (int j = 0; j < allItems[i].activeItems.Length; j++)
                    {
                        allItems[i].activeItems[j].SetActive(true);
                    }
                    allItems[i].active = true;
                    ItemNumber++;
                }
            }
        }
        public void DowngradeItem() 
        {
            ItemNumber--;
            ItemNumber = Mathf.Clamp(ItemNumber, 0, ItemNumber);

            for (int i = 0; i < allItems[ItemNumber].diactiveItems.Length; i++)
            {
                allItems[ItemNumber].diactiveItems[i].SetActive(false);
            }
            for (int i = 0; i < allItems[ItemNumber].activeItems.Length; i++)
            {
                allItems[ItemNumber].activeItems[i].SetActive(true);
            }
        }
    }

}
[System.Serializable]
public class ItemHolder
{
    [SerializeField] string name;
    public int targetLevel;
    public GameObject[] activeItems;
    public GameObject[] diactiveItems;

    public bool active;
}