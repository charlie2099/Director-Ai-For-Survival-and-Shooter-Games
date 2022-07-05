using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player _player;

    private void OnEnable()
    {
        _player.TakenDamage += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _player.TakenDamage -= UpdateHealthBar;
    }

    private void Awake()
    {
        _player = gameObject.GetComponent<Player>();
    }

    private void UpdateHealthBar()
    {
        slider.maxValue = _player.GetMaxHealth();
        slider.value = _player.GetHealth();
    }
}
