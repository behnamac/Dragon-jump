using System;
using DG.Tweening;
using Enums;
using Levels;
using Storage;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class UiController : MonoBehaviour
    {
        #region PUBLIC PROPS

        public static UiController Instance { get; private set; }

        #endregion

        #region SERIALIZE FIELDS

        [Header("Panels")]
        [SerializeField] private GameObject gamePlayPanel;

        [SerializeField] private GameObject levelStartPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject levelFailPanel;
        [SerializeField] private GameObject tutorialPanel;
        
        [Header("Coin")]
        [SerializeField] private Image coinIcon;

        [SerializeField] private Text coinText;
        [SerializeField] private Image[] levelFinishCoins;
        [SerializeField] private Text levelFinishCoinText;

        [Header("Level")]
        [SerializeField] private Text levelText;

        [Header("Settings Value")]
        [SerializeField] private int hideTutorialLevelIndex;

        [SerializeField] private float levelCompletePanelShowDelayTime;
        [SerializeField] private float levelFailPanelShowDelayTime;

        #endregion

        #region PRIVATE FIELDS

        private int _levelFinishTotalCount;
        private int _thisLevelCoinNumber;
        private Vector3 _firstCoinScale;
       // private StorageManager _storageManager;

        #endregion

        #region PRIVATE METHODS

        private void Initializer()
        {
            // Level Start
            levelStartPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                levelStartPanel.SetActive(false);
                LevelManager.Instance.LevelStart();
            });

            // Level Complete
            levelCompletePanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

            // Level Fail
            levelFailPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

            // Set TotalCoin;
            coinText.text = PlayerPrefsController.GetTotalCurrency().ToString();

            // Set Level Number
            var levelNumber = PlayerPrefsController.GetLevelNumber();
            levelText.text = $"LEVEL {levelNumber}";

            _firstCoinScale = coinIcon.transform.localScale;
        }

        private void ShowTutorial()
        {
            if (PlayerPrefsController.GetLevelIndex() > hideTutorialLevelIndex) return;
            tutorialPanel.SetActive(true);

            Invoke(nameof(HideTutorial), 2);
        }

        private void HideTutorial()
        {
            tutorialPanel.SetActive(false);
        }

        private void MoveLevelFinishCoins()
        {
            for (var i = 0; i < levelFinishCoins.Length; i++)
            {
                var coin = levelFinishCoins[i];

                if (i < levelFinishCoins.Length - 1)
                {
                    coin.transform.DOMove(coinIcon.transform.position, 0.3f + i / 10f).SetDelay(0.5f);
                    continue;
                }

                coin.transform.DOMove(coinIcon.transform.position, 0.3f + i / 10f).SetDelay(0.5f).OnComplete(() =>
                {
                    AddCoin(Convert.ToInt32(levelFinishCoinText.text));
                });
            }
        }

        private void ShowLevelCompletePanel()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }

            levelCompletePanel.SetActive(true);
        }

        private void ShowLevelFailPanel()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }

            levelFailPanel.SetActive(true);
        }

        #endregion

        #region PUBLIC METHODS

        public void AddCoin(int coinCount)
        {
            Haptic.HapticCollectCurrency();

            var totalCoin = PlayerPrefsController.GetTotalCurrency();

            totalCoin += coinCount;
            _thisLevelCoinNumber += coinCount;
            levelFinishCoinText.text = _thisLevelCoinNumber.ToString();

            PlayerPrefsController.SetCurrency(totalCoin);

            coinText.text = totalCoin.ToString();

            coinIcon.transform.DOScale(_firstCoinScale + Vector3.one * 0.5f, 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                coinIcon.transform.DOScale(_firstCoinScale, 0.2f).SetEase(Ease.InBounce);
            });
        }

        public void LevelFinishCoinCount(int coinCount)
        {
            Haptic.HapticCollectCurrency();

            _levelFinishTotalCount += coinCount;

            levelFinishCoinText.text = _levelFinishTotalCount.ToString();
        }

        public void SetLipCount(int count)
        {
            Haptic.HapticCollectCurrency();
        }

        public void StartLevelFinishCoins()
        {
            Invoke(nameof(MoveLevelFinishCoins), 0.5f);
        }

        #endregion

        #region CUSTOM EVENTS

        private void OnLevelFail(Level levelData)
        {
            Invoke(nameof(ShowLevelFailPanel), levelFailPanelShowDelayTime);
        }

        private void OnLevelStart(Level levelData)
        {
            ShowTutorial();
        }

        private void OnLevelComplete(Level levelData)
        {
            Invoke(nameof(ShowLevelCompletePanel), levelCompletePanelShowDelayTime);
        }

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            // TODO : IF DONT NEED THIS METHODS, YOU DONT REMOVE
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            //_storageManager = new StorageManager();

            Initializer();

            if (Instance == null) Instance = this;

        }

        private void Start()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;

            GameManager.OnStateChanged += CheckGameState;
        }


        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
            
            GameManager.OnStateChanged -= CheckGameState;
        }

        #endregion

        private void CheckGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    levelStartPanel.SetActive(true);
                    break;
                case GameState.Playing:
                    gamePlayPanel.SetActive(true);
                    break;
                case GameState.Win:
                    ShowLevelCompletePanel();
                    AddCoin(100);
                    break;
                case GameState.Lose:
                    ShowLevelFailPanel();
                    break;
                case GameState.RunToFinish:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}