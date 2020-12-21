using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gravity : MonoBehaviour
{
    private Vector3 _gravity;

    private void Start()
    {
        _gravity = Physics2D.gravity;
    }
    public void SetBaseGravity()
    {
        Physics2D.gravity = _gravity;
        Physics.gravity = _gravity;
    }

    public void SetReverseGravity()
    {
        Physics2D.gravity = -_gravity;
        Physics.gravity = -_gravity;
    }
}
