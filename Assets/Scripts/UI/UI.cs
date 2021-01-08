using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] Game _game;
    [SerializeField] Canvas _pauseScreen;
    [SerializeField] Canvas _gameOverScreen;
    [SerializeField] Canvas _inGameUI;
    [SerializeField] Canvas _main;

    private void Awake()
    {
        ToMainMenu();
    }

    private void OnEnable()
    {
        _game.GamePauseChanged += OnGamePauseChanged;
        _game.GameOver.AddListener(OnGameOver);
        _game.Restarted.AddListener(OnRestart);
    }

    private void OnDisable()
    {
        _game.GamePauseChanged -= OnGamePauseChanged;
        _game.GameOver.RemoveListener(OnGameOver);
        _game.Restarted.RemoveListener(OnRestart);
    }

    private void OnGamePauseChanged(bool state)
    {
        _pauseScreen.enabled = state;
    }

    private void OnGameOver()
    {
        _main.enabled = false;
        _pauseScreen.enabled = false;
        _gameOverScreen.enabled = true;
        _inGameUI.enabled = false;
    }

    private void OnRestart()
    {
        _main.enabled = false;
        _pauseScreen.enabled = false;
        _gameOverScreen.enabled = false;
        _inGameUI.enabled = true;
    }

    private void ToMainMenu()
    {
        _main.enabled = true;
        _pauseScreen.enabled = false;
        _gameOverScreen.enabled = false;
        _inGameUI.enabled = false;
    }
}
