using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]

public class PlayerInput : MonoBehaviour
{
    public UnityEvent PauseButtonPreesed;

    [SerializeField] Gravity _gravity;

    private Player _player;

    void Awake()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            _gravity.SetBaseGravity();
        else if (Input.GetKeyDown(KeyCode.W))
            _gravity.SetReverseGravity();
        if (Input.GetKeyDown(KeyCode.Space))
            PauseButtonPreesed?.Invoke();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            _player.AddForwardForce();
        else if (Input.GetKey(KeyCode.A))
            _player.Slow();   
    }
}
