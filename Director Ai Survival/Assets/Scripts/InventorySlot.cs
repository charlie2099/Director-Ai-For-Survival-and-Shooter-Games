using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject inventorySlotInfoBox;
    
    private Player _player;
    private Image _toolIcon;
    private Color _colour;
    private Transform _inventorySlotPanel;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _toolIcon = GameObject.Find("Tool_Icon").GetComponent<Image>();
        _inventorySlotPanel = transform.GetChild(0);
    }

    private void Start()
    {
        _colour = GetComponent<Image>().color;
        inventorySlotInfoBox.SetActive(false);
    }

    public void OnCursorEnter()
    {
        GetComponent<Image>().color = new Color(0.6f, 0.4f, 0);
        inventorySlotInfoBox.SetActive(true);
        inventorySlotInfoBox.transform.parent = _inventorySlotPanel;
        inventorySlotInfoBox.transform.position = _inventorySlotPanel.transform.position + new Vector3(60, 175, 20);
        inventorySlotInfoBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetComponent<ItemStack>().GetItemType().ToString();

        if (GetComponent<ItemStack>().GetItems().Count > 0)
        {
            inventorySlotInfoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetComponent<ItemStack>().GetItems()[0].GetItemInfo();
        }
    }
    
    public void OnCursorExit()
    {
        GetComponent<Image>().color = _colour;
        inventorySlotInfoBox.SetActive(false);
    }

    public void OnCursorPress()
    {
        GetComponent<Image>().color = new Color(0f, 0.24f,0.34f);
        if (GetComponent<ItemStack>().GetCurrentStackSize() > 0)
        {
            _player.SetItemInHand(GetComponent<ItemStack>().GetItems()[GetComponent<ItemStack>().GetItems().Count-1]);
            _toolIcon.enabled = true;
            _toolIcon.sprite = GetComponent<ItemStack>().transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        }
    }

    public void OnCursorRelease()
    {
        GetComponent<Image>().color = _colour;
    }
}
