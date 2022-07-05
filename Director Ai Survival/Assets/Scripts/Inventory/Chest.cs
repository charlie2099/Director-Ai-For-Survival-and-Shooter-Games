using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    public List<ItemStack> chestInventorySlots = new List<ItemStack>();
    [SerializeField] private GameObject chestInventoryUi;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Sprite chestOpenSprite;
    [SerializeField] private Sprite chestClosedSprite;
    [SerializeField] private GameObject playerInventoryContainer;

    [Space] 
    [SerializeField] private GameObject[] itemsToSpawn;
    
    private SpriteRenderer _chestSpriteRenderer;
    private Text _uiPanelText;
    private bool _isInRange;
    private bool _isOpen;
    private bool _mouseIsOver;
    
    private void OnEnable()
    {
        foreach (var slot in chestInventorySlots)
        {
            slot.FirstItemAddedToStack += SetInventorySlotImage;
            slot.ItemStackChange += UpdateStackSize;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in chestInventorySlots)
        {
            slot.FirstItemAddedToStack -= SetInventorySlotImage;
            slot.ItemStackChange -= UpdateStackSize;
        }
    }

    private void Awake()
    {
        _chestSpriteRenderer = GetComponent<SpriteRenderer>();
        _uiPanelText = uiPanel.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        chestInventoryUi.SetActive(false);
        
        GameObject chestItem = Instantiate(itemsToSpawn[0], transform.position, Quaternion.identity);
        chestItem.transform.parent = transform;
        if (chestItem.GetComponent<Sword>() != null)
        {
            chestItem.GetComponent<Sword>().SetItemType(ItemType.Type.SWORD);
            chestItem.GetComponent<Sword>().SetMaxStackSize(1);
            AddToStackEvent(chestItem.GetComponent<Sword>());
        }
        
        GameObject chestItem2 = Instantiate(itemsToSpawn[1], transform.position, Quaternion.identity);
        chestItem2.transform.parent = transform;
        if (chestItem2.GetComponent<Pickaxe>() != null)
        {
            chestItem2.GetComponent<Pickaxe>().SetItemType(ItemType.Type.PICKAXE);
            chestItem2.GetComponent<Pickaxe>().SetMaxStackSize(1);
            AddToStackEvent(chestItem2.GetComponent<Pickaxe>());
        }
        
        GameObject chestItem3 = Instantiate(itemsToSpawn[2], transform.position, Quaternion.identity);
        chestItem3.transform.parent = transform;
        if (chestItem3.GetComponent<Axe>() != null)
        {
            chestItem3.GetComponent<Axe>().SetItemType(ItemType.Type.AXE);
            chestItem3.GetComponent<Axe>().SetMaxStackSize(1);
            AddToStackEvent(chestItem3.GetComponent<Axe>());
        }
        
        for (int i = 0; i < Random.Range(16, 17); i++)
        {
            GameObject chestItems = Instantiate(itemsToSpawn[Random.Range(3,itemsToSpawn.Length)], transform.position, Quaternion.identity);
            chestItems.transform.parent = transform;
            //chestItem.transform.position = new Vector3(chestItem.transform.position.x, chestItem.transform.position.y + 1);

            // TODO: Refactor!
            if (chestItems.GetComponent<Stone>() != null)
            {
                chestItems.GetComponent<Stone>().SetItemType(ItemType.Type.STONE);
                AddToStackEvent(chestItems.GetComponent<Stone>());
            }
            if (chestItems.GetComponent<Wood>() != null)
            {
                chestItems.GetComponent<Wood>().SetItemType(ItemType.Type.WOOD);
                AddToStackEvent(chestItems.GetComponent<Wood>());
            }
            if (chestItems.GetComponent<Apple>() != null)
            {
                chestItems.GetComponent<Apple>().SetItemType(ItemType.Type.APPLE);
                AddToStackEvent(chestItems.GetComponent<Apple>());
            }
            if (chestItems.GetComponent<Gold>() != null)
            {
                chestItems.GetComponent<Gold>().SetItemType(ItemType.Type.GOLD);
                AddToStackEvent(chestItems.GetComponent<Gold>());
            }
            /*else
            {
                Destroy(chestItem);
            }*/
        }
    }

    private void Update()
    {
        if (_isInRange)
        {
            uiPanel.SetActive(true);
            uiPanel.transform.position = transform.position + new Vector3(0, -0.7f);
            _uiPanelText.text = "Press SPACE";

            // Open chest
            if (Input.GetKeyDown(KeyCode.Space) && !_isOpen)
            {
                _chestSpriteRenderer.sprite = chestOpenSprite;
                chestInventoryUi.SetActive(true);
                _isOpen = true;
            }
            // Close chest
            else if (Input.GetKeyDown(KeyCode.Space) && _isOpen)
            {
                _chestSpriteRenderer.sprite = chestClosedSprite;
                chestInventoryUi.SetActive(false);
                _isOpen = false;
            }
            
            // Add items to player inventory
            if (Input.GetKeyDown(KeyCode.Q) && _isOpen)
            {
                foreach (var chestSlot in chestInventorySlots.ToList())
                {
                    foreach (var item in chestSlot.itemStackList.ToList())
                    {
                        InventoryResourceCache.Instance.AddToCache(item);
                        chestSlot.RemoveFromStack(item);
                        chestSlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                        item.transform.parent = playerInventoryContainer.transform;
                        item.gameObject.SetActive(false);
                    }
                }
            }

            // Destroy chest if all items have been removed
            if (chestInventorySlots[0].GetItems().Count <= 0 &&
                chestInventorySlots[1].GetItems().Count <= 0 &&
                chestInventorySlots[2].GetItems().Count <= 0 &&
                chestInventorySlots[3].GetItems().Count <= 0 &&
                chestInventorySlots[4].GetItems().Count <= 0 &&
                chestInventorySlots[5].GetItems().Count <= 0)
            {
                uiPanel.SetActive(false);
                Destroy(gameObject);
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
        itemStack.SetMaxStackSize(item.GetMaxStackSize());
    }

    private void UpdateStackSize(ItemStack itemStack)
    {
        // TODO: Temporary feature testing code! Refactor!
        itemStack.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemStack.GetCurrentStackSize().ToString();
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
            if (itemStack.GetCurrentStackSize() >= itemStack.GetMaxStackSize())
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
