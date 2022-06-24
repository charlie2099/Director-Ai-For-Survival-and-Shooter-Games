using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Image inventoryBackpackIcon;
    [SerializeField] private Sprite inventoryBackpackOpenSprite;
    [SerializeField] private Sprite inventoryBackpackClosedSprite;
    private bool _inventoryIsOpen = false;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_inventoryIsOpen)
        {
            inventoryPanel.SetActive(true);
            inventoryBackpackIcon.sprite = inventoryBackpackOpenSprite;
            _inventoryIsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _inventoryIsOpen)
        {
            inventoryPanel.SetActive(false);
            inventoryBackpackIcon.sprite = inventoryBackpackClosedSprite;
            _inventoryIsOpen = false;
        }
    }
}
