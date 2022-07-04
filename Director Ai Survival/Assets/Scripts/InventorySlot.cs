using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Player _player;
    private Image _toolIcon;
    private Color _colour;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _toolIcon = GameObject.Find("Tool_Icon").GetComponent<Image>();
    }

    private void Start()
    {
        _colour = GetComponent<Image>().color;
    }

    public void OnCursorEnter()
    {
        GetComponent<Image>().color = new Color(0.6f, 0.4f, 0);
    }
    
    public void OnCursorExit()
    {
        GetComponent<Image>().color = _colour;
    }

    public void OnCursorPress()
    {
        GetComponent<Image>().color = new Color(0f, 0.24f,0.34f);
        print("Item: " + GetComponent<ItemStack>().GetItemType());

        if (GetComponent<ItemStack>().GetCurrentStackSize() > 0)
        {
            _player.SetItemInHand(GetComponent<ItemStack>().GetItems()[0]);
            _toolIcon.sprite = GetComponent<ItemStack>().transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        }
    }

    public void OnCursorRelease()
    {
        GetComponent<Image>().color = _colour;
    }
}
