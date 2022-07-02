using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player _player;

    private void OnEnable()
    {
        _player.EnergyUsed += UpdateEnergyBar;
    }

    private void OnDisable()
    {
        _player.EnergyUsed -= UpdateEnergyBar;
    }

    private void Awake()
    {
        _player = gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        if (slider.value < slider.maxValue)
        {
            slider.value += 5 * Time.deltaTime;
        }
    }

    private void UpdateEnergyBar()
    {
        slider.maxValue = _player.GetMaxEnergy();
        slider.value = _player.GetEnergy();
    }
}
