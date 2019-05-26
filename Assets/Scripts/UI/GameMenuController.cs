using System.Collections;
using System.Collections.Generic;
using SimpleSpaceShooter.Common;
using SimpleSpaceShooter.Manager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using SimpleSpaceShooter.Core;

namespace SimpleSpaceShooter.UI {
    
    public class GameMenuController : MonoBehaviour {

        private GameObject pauseScreen;
        private GameObject resultScreen;
        private TextMeshProUGUI scoreText;
        private Image scoreFillBar;
        private Image healthFillBar;

        private GameManager gameManager;
        private LevelManager levelManager;

        void Awake() {
            gameManager = GetComponent<GameManager>();
            levelManager = GetComponent<LevelManager>();

            Canvas canvas = FindObjectOfType<Canvas>();
            pauseScreen = canvas.transform.Find("PauseScreen").gameObject;
            resultScreen = canvas.transform.Find("ResultScreen").gameObject;
            scoreText = canvas.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            scoreFillBar = canvas.transform.Find("ScoreBar").Find("FillBar").GetComponent<Image>();
            healthFillBar = canvas.transform.Find("HealthBar").Find("FillBar").GetComponent<Image>();
        }

        void Start() {
            pauseScreen.SetActive(false);
            resultScreen.SetActive(false);

            GameObject player = GameObject.FindWithTag("Player");
            Health playerHealth = player.GetComponent<Health>();

            playerHealth.OnChangeHealth += UpdatePlayerHealth;
            gameManager.OnGameLostAction += ShowResultScreen;
            levelManager.OnScoreChange += UpdateScore;
            
            UpdatePlayerHealth(playerHealth);
            UpdateScore(levelManager.CurrentPoints);
        }

        void Update() {
            if (!gameManager.Finished && Input.GetKeyDown(KeyCode.Escape)) {
                if (gameManager.Paused) {
                    ResumeGame();
                } else {
                    PauseGame();
                }
            }
        }

        private void UpdateScore(int newScore) {
            scoreText.text = newScore.ToString().PadLeft(5, '0');
            scoreFillBar.fillAmount = (float) newScore / levelManager.PointsToFinish;
        }

        private void UpdatePlayerHealth(Health playerHealth) {
            healthFillBar.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        }

        private void PauseGame() {
            pauseScreen.SetActive(true);
            gameManager.PauseGame();
        }

        public void ResumeGame() {
            pauseScreen.SetActive(false);
            gameManager.ResumeGame();
        }

        public void ShowResultScreen() {
            pauseScreen.SetActive(false);
            resultScreen.SetActive(true);
        }

        public void RestartGame() {
            SceneManager.LoadScene(Constants.SCENE_INDEX_GAME);
        }

        public void QuitGame() {
            SceneManager.LoadScene(Constants.SCENE_INDEX_MAIN_MENU);
        }

    }

}