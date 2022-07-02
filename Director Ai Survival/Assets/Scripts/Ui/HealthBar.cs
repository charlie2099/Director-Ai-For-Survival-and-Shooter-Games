using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
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
        slider.value = _player.GetHealth();
        slider.maxValue = _player.GetMaxHealth();
    }
}
