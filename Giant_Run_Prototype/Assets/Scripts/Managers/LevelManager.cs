using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    private GameObject _boss;

    private bool _isGameEnd = false;
    public static Action<bool> OnGameEnd;

    private void Start() {
        _isGameEnd = false;
    }

    private void Update() {
        if (_isGameEnd && Input.touchCount >= 1) {
            RestartGame();
        }
    }

    private void LevelEnd(CharacterController _character) {
        bool isWin = !(_boss.transform.localScale.x > _character.transform.localScale.x);
        _isGameEnd = true;
        OnGameEnd?.Invoke(isWin);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    private void OnEnable() {
        CharacterController.OnEndPointReached += LevelEnd;
    }

    private void OnDisable() {
        CharacterController.OnEndPointReached -= LevelEnd;
    }
}