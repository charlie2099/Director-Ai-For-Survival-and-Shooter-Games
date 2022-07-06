using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Merchant : MonoBehaviour
{
    [Serializable]
    public struct ShopItem
    {
        public GameObject itemToSell;
        public GameObject resourceCost;
        public ItemStack merchantInventorySlot;
        public ItemStack merchantResourceSlot;
        public int resourcesRequired;
    }
    [SerializeField] private ShopItem shopItem;
    
    [Space]
    [SerializeField] private GameObject merchantInventoryUi;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject playerInventoryContainer;

    private InventorySystem _playerInventorySystem;
    private Text _uiPanelText;
    private int _totalResourcesRequired;
    private bool _isInRange;
    private bool _isOpen;
    private bool _mouseIsOver;
    
    private void OnEnable()
    {
        shopItem.merchantInventorySlot.FirstItemAddedToStack += SetInventorySlotImage;
        shopItem.merchantInventorySlot.ItemStackChange += UpdateStackSize;
        
        shopItem.merchantResourceSlot.FirstItemAddedToStack += SetInventorySlotImage;
        shopItem.merchantResourceSlot.ItemStackChange += UpdateStackSize;
    }

    private void OnDisable()
    {
        shopItem.merchantInventorySlot.FirstItemAddedToStack -= SetInventorySlotImage;
        shopItem.merchantInventorySlot.ItemStackChange -= UpdateStackSize;
        
        shopItem.merchantResourceSlot.FirstItemAddedToStack -= SetInventorySlotImage;
        shopItem.merchantResourceSlot.ItemStackChange -= UpdateStackSize;
    }

    private void Awake()
    {
        _uiPanelText = uiPanel.GetComponentInChildren<Text>();
        _playerInventorySystem = playerInventoryContainer.transform.parent.GetComponent<InventorySystem>();
    }

    private void Start()
    {
        merchantInventoryUi.SetActive(false);

        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            GameObject merchantItem = Instantiate(shopItem.itemToSell, transform.position, Quaternion.identity);
            merchantItem.transform.parent = transform;
        
            if (merchantItem.GetComponent<CannonItem>() != null)
            {
                merchantItem.GetComponent<CannonItem>().SetItemType(ItemType.Type.CANNON);
                //merchantItem.GetComponent<CannonItem>().SetMaxStackSize(1);
                AddToShopStack(merchantItem.GetComponent<CannonItem>());
            }
        }
        
        _totalResourcesRequired = shopItem.resourcesRequired * shopItem.merchantInventorySlot.GetCurrentStackSize();
        
        for (int i = 0; i < _totalResourcesRequired; i++)
        {
            GameObject merchantResourceCostItem = Instantiate(shopItem.resourceCost, transform.position, Quaternion.identity);
            merchantResourceCostItem.transform.parent = transform;
        
            if (merchantResourceCostItem.GetComponent<Gold>() != null)
            {
                merchantResourceCostItem.GetComponent<Gold>().SetItemType(ItemType.Type.GOLD);
                AddToResourceStack(merchantResourceCostItem.GetComponent<Gold>());
            }
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
                merchantInventoryUi.SetActive(true);
                _isOpen = true;
            }
            // Close chest
            else if (Input.GetKeyDown(KeyCode.Space) && _isOpen)
            {
                merchantInventoryUi.SetActive(false);
                _isOpen = false;
            }
            
            // Add items to player inventory
            if (Input.GetKeyDown(KeyCode.Q) && _isOpen && FindItemInPlayerInventory(ItemType.Type.GOLD) >= _totalResourcesRequired)
            {
                foreach (var item in shopItem.merchantInventorySlot.itemStackList.ToList())
                {
                    InventoryResourceCache.Instance.AddToCache(item);
                    shopItem.merchantInventorySlot.RemoveFromStack(item);
                    shopItem.merchantInventorySlot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
                    item.transform.parent = playerInventoryContainer.transform;
                    item.gameObject.SetActive(false);
                }
                RemoveItemFromPlayerInventory(ItemType.Type.GOLD);
            }

            // Destroy chest if all items have been removed
            if (shopItem.merchantInventorySlot.GetItems().Count <= 0)
            {
                uiPanel.SetActive(false);
                merchantInventoryUi.SetActive(false);
                Destroy(gameObject);
            }
            
        }
        else
        {
            merchantInventoryUi.SetActive(false);
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
    
    private void AddToShopStack(Item item)
    {
        shopItem.merchantInventorySlot.AddToStack(item);
    }
    
    private void AddToResourceStack(Item item)
    {
        shopItem.merchantResourceSlot.AddToStack(item);
    }

    public int FindItemInPlayerInventory(ItemType.Type itemTypeToFind)
    {
        int totalItemsInPlayerInventory = 0;
        foreach (var slot in _playerInventorySystem.inventorySlots)
        {
            foreach (var item in slot.itemStackList)
            {
                if (item.GetItemType() == itemTypeToFind)
                {
                    totalItemsInPlayerInventory++;
                    print( "Slot: " + slot + ", Item: " + item + ", Stack Size: " + slot.GetCurrentStackSize());
                }
            }
        }
        print("Items found: " + totalItemsInPlayerInventory);
        return totalItemsInPlayerInventory;
    }
    
    public void RemoveItemFromPlayerInventory(ItemType.Type itemTypeToRemove)
    {
        int totalItemsInPlayerInventory = 0;
        foreach (var itemStack in _playerInventorySystem.inventorySlots)
        {
            foreach (var item in itemStack.GetItems().ToList())
            {
                if (item.GetItemType() == itemTypeToRemove && totalItemsInPlayerInventory < _totalResourcesRequired)
                {
                    InventoryResourceCache.Instance.ItemToRemoveFromInv(itemStack, item);
                    totalItemsInPlayerInventory++;
                }
            }
        }
    }
}
