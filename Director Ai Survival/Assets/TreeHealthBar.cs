using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Tree _tree;

    private void Awake()
    {
        _tree = gameObject.GetComponent<Tree>();
    }

    private void Start()
    {
        //slider.gameObject.GetComponentInParent<GameObject>().SetActive(false);
    }

    private void Update()
    {
        UpdateHealthBar();

        if (_tree.GetHealth() <= _tree.GetMaxHealth())
        {
            //slider.gameObject.GetComponentInParent<GameObject>().SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        slider.value = _tree.GetHealth();
        slider.maxValue = _tree.GetMaxHealth();
    }
}
