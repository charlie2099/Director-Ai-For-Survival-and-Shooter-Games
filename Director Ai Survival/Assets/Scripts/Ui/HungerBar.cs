using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player _player;

    private void OnEnable()
    {
        //_player.EnergyUsed += UpdateHungerBar;
    }

    private void OnDisable()
    {
        //_player.EnergyUsed -= UpdateHungerBar;
    }

    private void Awake()
    {
        _player = gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        if (slider.value >= 0)
        {
            slider.value -= 0.01f * Time.deltaTime;
        }
    }

    private void UpdateHungerBar()
    {
        slider.maxValue = _player.GetMaxHunger();
        slider.value = _player.GetHunger();
    }
}
