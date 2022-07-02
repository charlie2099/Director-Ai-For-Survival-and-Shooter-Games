using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Items;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public List<ItemStack> chestInventorySlots = new List<ItemStack>();
    [SerializeField] private GameObject chestInventoryUi;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Sprite chestOpenSprite;
    [SerializeField] private Sprite chestClosedSprite;

    [Space] 
    [SerializeField] private GameObject itemsToSpawn;
    
    private SpriteRenderer _chestSpriteRenderer;
    private Text _uiPanelText;
    private const int STACK_SIZE = 16;
    private bool _isInRange;
    private bool _isOpen;
    private bool _mouseIsOver;

    private void Awake()
    {
        _chestSpriteRenderer = GetComponent<SpriteRenderer>();
        _uiPanelText = uiPanel.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        chestInventoryUi.SetActive(false);
        
        foreach (var slot in chestInventorySlots)
        {
            slot.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.GetStackSize().ToString();
        }

        GameObject chestItem = Instantiate(itemsToSpawn, transform.position, Quaternion.identity);
        chestItem.transform.parent = transform;
        chestItem.transform.position = new Vector3(chestItem.transform.position.x, chestItem.transform.position.y + 1);

        //AddToStackEvent(chestItem.GetComponent<Item>());
        SetInventorySlotImage(chestInventorySlots[0], chestItem.GetComponent<Item>()); 
        UpdateStackSize(chestInventorySlots[0]);
    }

    private void Update()
    {
        if (_isInRange)
        {
            uiPanel.SetActive(true);
            uiPanel.transform.position = transform.position + new Vector3(0, -0.7f);
            _uiPanelText.text = "Press SPACE";

            if (Input.GetKeyDown(KeyCode.Space) && !_isOpen)
            {
                _chestSpriteRenderer.sprite = chestOpenSprite;
                chestInventoryUi.SetActive(true);
                _isOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _isOpen)
            {
                _chestSpriteRenderer.sprite = chestClosedSprite;
                chestInventoryUi.SetActive(false);
                _isOpen = false;
            }
            
            if (Input.GetKeyDown(KeyCode.Q) && _isOpen)
            {
                print("Items collected");
                //InventoryResourceCache.Instance.AddToCache(this);
            }
        }
        else
        {
            chestInventoryUi.SetActive(false);
            _chestSpriteRenderer.sprite = chestClosedSprite;
            _isOpen = false;
            
            if (!_mouseIsOver)
            {
                uiPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
    
    private void OnMouseOver()
    {
        uiPanel.SetActive(true);
        _uiPanelText.text = gameObject.name;
        uiPanel.transform.position = transform.position + new Vector3(0, -0.7f);
        _mouseIsOver = true;
    }

    private void OnMouseExit()
    {
        uiPanel.SetActive(false);
        _mouseIsOver = false;
    }
    
    private void SetInventorySlotImage(ItemStack itemStack, Item item)
    {
        // Items sprite renderer is destroyed on pickup (disable instead?)
        // TODO: Temporary feature testing code! Refactor!
        itemStack.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
        itemStack.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    private void UpdateStackSize(ItemStack itemStack)
    {
        // TODO: Temporary feature testing code! Refactor!
        itemStack.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemStack.GetStackSize().ToString();
        //print("<color=green>" + itemStack.transform.name + "</color>, <color=cyan>Stack size: </color>" + itemStack.GetStackSize());
    }
    
    private void AddToStackEvent(Item item)
            {
                IsStackFull(item);
            }
    
            private bool IsStackFull(Item item)
            {
                // The first item to enter the stack defines the ItemType
                // for that stack until the last of that item has been
                // removed from the stack
                
                int stackCounter = 0;
                
                for (var i = 0; i < chestInventorySlots.Count; i++)
                {
                    ItemStack itemStack = chestInventorySlots[stackCounter];
    
                    // Add to next stack if current is full
                    if (itemStack.GetStackSize() >= STACK_SIZE)
                    {
                        stackCounter++;
                    }    
                    else
                    {
                        if (itemStack.GetItems().Count <= 0)
                        {
                            itemStack.AddToStack(item);
                            itemStack.SetStackItemType(item.GetItemType());
                            return true;
                        }
                        
                        if (itemStack.GetItems().Count > 0)
                        {
                            if (item.GetItemType() == itemStack.GetItemType())
                            {
                                // If item to enter the stack is the same ItemType to the first,
                                // enter the stack. 
                                itemStack.AddToStack(item);
                                return true;
                            }
                            if (item.GetItemType() != itemStack.GetItemType())
                            {
                                stackCounter++;
                            }
                        }
                    }
                }
                return false;
            }
}
