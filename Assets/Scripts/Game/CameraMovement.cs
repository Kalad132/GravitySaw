using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxPlayerSpeed;
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _offsetChangeSpeed;
    [SerializeField] private Background _background;
    [SerializeField] private Game _game;

    private float _currentOffset = 0;
    private Vector3 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
    }

    private void OnEnable()
    {
        _game.Restarted.AddListener(ResetPosition);
    }


    private void OnDisable()
    {
        _game.Restarted.RemoveListener(ResetPosition);
    }

    private void LateUpdate()
    {
        UpdateOffset();
        Vector3 direction = new Vector3 (_playerTransform.position.x + _currentOffset - transform.position.x, 0, 0);
        if (Mathf.Abs(direction.x) > _minDistance)
        {
            Vector3 moveVector = direction * _moveSpeed;
            transform.Translate(moveVector * Time.deltaTime);
            _background.FollowCamera(moveVector);
        }
    }

    private void ResetPosition()
    {
        transform.position = _startingPosition;
    }

    private void UpdateOffset()
    {
        float targetOffset = Mathf.Lerp(0f, _maxOffset, _player.GetHorisontalSpeed() / _maxPlayerSpeed);
        _currentOffset = Mathf.Lerp(_currentOffset, targetOffset, _offsetChangeSpeed * Time.deltaTime);

    }
}
