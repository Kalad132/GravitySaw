using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _changeTime;

    private Slider _slider;
    private Coroutine _working;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _player.HealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        _player.HealthChanged.RemoveListener(OnHealthChanged);
    }

    private void OnHealthChanged(int current, int max)
    {
        if (_working != null)
            StopCoroutine(_working);
        _working = StartCoroutine(BarChange((float)current / max));
    }

    private IEnumerator BarChange(float targetValue)
    {
        float passedTime = 0;
        float starPoint = _slider.value;
        while (_slider.value != targetValue)
        {
            passedTime += Time.deltaTime;
            _slider.value = Mathf.Lerp(starPoint, targetValue, passedTime / _changeTime);
            yield return null;
        }
    }
}
