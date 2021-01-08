using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityAction<bool> GamePauseChanged;
    public UnityEvent Restarted = new UnityEvent();
    public UnityEvent GameOver = new UnityEvent();

    [SerializeField] private PlayerInput _input;
    [SerializeField] private Player _player;

    private bool _pause => Time.timeScale == 1 ? false : true;

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Restarted?.Invoke();
        Time.timeScale = 1;
    }

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _input.PauseButtonPreesed.AddListener(OnPauseButtonPressed);
        _player.Death.AddListener(OnPlayerDeath);
    }

    private void OnDisable()
    {
        _input.PauseButtonPreesed.RemoveListener(OnPauseButtonPressed);
        _player.Death.RemoveListener(OnPlayerDeath);
    }

    private void OnPauseButtonPressed()
    {
        Time.timeScale = _pause ? 1 : 0;
        GamePauseChanged?.Invoke(_pause);
    }

    private void OnPlayerDeath()
    {
        Time.timeScale = 0;
        GameOver?.Invoke();
    }
}
