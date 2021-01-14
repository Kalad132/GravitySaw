using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public UnityAction<int, int> HealthChanged;
    public UnityAction<int> ScoreChanged;
    public UnityEvent Death = new UnityEvent();

    [SerializeField] private Game _game;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _startingForceMultipier;
    [SerializeField] private float _targetStartingSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSlowSpeed;
    [SerializeField] private float _maxSlowingForce;
    [SerializeField] private ParticleSystem _death;

    private Rigidbody2D _rigidbody;
    private int _currentHealth;
    private int _score;
    private Vector3 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        _game.Restarted.AddListener(Respawn);
        Respawn();
    }

    private void OnDestroy()
    {
        _game.Restarted.RemoveListener(Respawn);
    }

    public void Slow()
    {
        if (_rigidbody.velocity.x > _minSlowSpeed)
        {
            float force = Mathf.Lerp(0, _maxSlowingForce, _rigidbody.velocity.x / _maxSpeed);
            _rigidbody.AddForce(Vector3.left * force * Time.fixedDeltaTime);
        }
    }

    public void AddForwardForce()
    {
        float force = _forwardForce;
        if (_rigidbody.velocity.x < _targetStartingSpeed)
            force *= _startingForceMultipier;
        if (_rigidbody.velocity.magnitude < _maxSpeed)
            _rigidbody.AddForce(Vector2.right * force * Time.fixedDeltaTime);
    }

    public float GetHorisontalSpeed()
    {
        return _rigidbody.velocity.x;
    }

    private void Respawn()
    {
        _currentHealth = _maxHealth;
        transform.position = _startingPosition;
        gameObject.SetActive(true);
        _score = 0;
        AddScore(0);
        TakeDamage(0);

    }

    private void TakeDamage(int damage)
    {
        if (damage < 0)
            return;
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        if (_currentHealth == 0)
            Die();
    }

    private void AddScore(int score)
    {
        if (score > 0)
            _score += score;
        ScoreChanged?.Invoke(_score);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bomb bomb))
            TakeDamage(bomb.Damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Skull skull))
            AddScore(1);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Instantiate(_death, transform.position, Quaternion.identity);
        Death?.Invoke();
    }

}

// Git testing
