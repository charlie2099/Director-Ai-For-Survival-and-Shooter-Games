using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Image inventoryBackpackIcon;
    [SerializeField] private Sprite inventoryBackpackOpenSprite;
    [SerializeField] private Sprite inventoryBackpackClosedSprite;
    public List<ItemStack> inventorySlots = new List<ItemStack>();
    
    private bool _inventoryIsOpen;

    private void OnEnable()
    {
        inventorySlots[0].FirstItemAddedToStack += SetInventorySlotImage;
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            print("Inventory Slot " + i + " stack size: " + inventorySlots[i].GetStackSize());
        }

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

    public void SetInventorySlotImage(Item item/*SpriteRenderer inventorySlotSprite, Sprite newSlotSprite*/)
    {
        print("Item Name: " + item.name);
        inventorySlots[0].GetComponentInChildren<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

        // Items sprite renderer is destroyed on pickup (disable instead?)
    }
}
