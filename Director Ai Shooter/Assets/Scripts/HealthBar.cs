using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color low;
    [SerializeField] private Color high;

    private Player _player;

    private void Awake()
    {
        _player = gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        slider.value = _player.GetCurrentHealth();
        slider.maxValue = _player.GetMaxHealth();

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}

