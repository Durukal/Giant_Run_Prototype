using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Managers {
    public class UIManager : MonoBehaviour {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private GameObject endScreen;

        [SerializeField]
        private TextMeshProUGUI endScreenText;
        [SerializeField]
        private GameObject startScreen;

        private bool _gameStarted = false;

        private int _score;

        private void Start() {
            _score = 1;
            scoreText.SetText("Height:" + _score.ToString());
            Time.timeScale = 0;
            startScreen.SetActive(true);
            _gameStarted = false;
        }

        private void Update() {
            if (!_gameStarted && Input.touchCount >= 1) {
                startScreen.SetActive(false);
                _gameStarted = true;
                Time.timeScale = 1;
            }
        }

        private void UpdateScore(int score) {
            _score += score;
            scoreText.SetText("Height:" + _score.ToString());
        }

        private void ShowGameEndUI(bool isWin) {
            if (isWin) {
                endScreenText.SetText("You Win!!");
            } else {
                endScreenText.SetText("You Lose!!");
            }

            endScreen.gameObject.SetActive(true);
        }

        private void OnEnable() {
            CharacterController.OnScoreUpdate += UpdateScore;
            LevelManager.OnGameEnd += ShowGameEndUI;
        }

        private void OnDisable() {
            CharacterController.OnScoreUpdate -= UpdateScore;
            LevelManager.OnGameEnd -= ShowGameEndUI;
        }
    }
}