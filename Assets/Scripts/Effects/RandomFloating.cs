using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RandomFloating : StateMachineBehaviour
{
    [SerializeField] private float _minSpeed = 0.7f;
    [SerializeField] private float _maxSpeed = 1.2f;
        
    private void OnStateEnter(Animator animator) 
    {
        animator.speed = Random.Range(_minSpeed, _maxSpeed);
    }
}
