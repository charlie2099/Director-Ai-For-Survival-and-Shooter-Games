using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    public List<ItemStack> merchantInventorySlots = new List<ItemStack>();
    [SerializeField] private GameObject merchantInventoryUi;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject playerInventoryContainer;

    [Space] 
    [SerializeField] private GameObject[] itemsToSpawn;
    
    private SpriteRenderer merchantSpriteRenderer;
    private Text _uiPanelText;
    private bool _isInRange;
    private bool _isOpen;
    private bool _mouseIsOver;
    
    private void OnEnable()
    {
        foreach (var slot in merchantInventorySlots)
        {
            slot.FirstItemAddedToStack += SetInventorySlotImage;
            slot.ItemStackChange += UpdateStackSize;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in merchantInventorySlots)
        {
            slot.FirstItemAddedToStack -= SetInventorySlotImage;
            slot.ItemStackChange -= UpdateStackSize;
        }
    }

    private void Awake()
    {
        merchantSpriteRenderer = GetComponent<SpriteRenderer>();
        _uiPanelText = uiPanel.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        merchantInventoryUi.SetActive(false);
        
        GameObject merchantItem = Instantiate(itemsToSpawn[0], transform.position, Quaternion.identity);
        merchantItem.transform.parent = transform;
        if (merchantItem.GetComponent<CannonItem>() != null)
        {
            merchantItem.GetComponent<CannonItem>().SetItemType(ItemType.Type.CANNON);
            merchantItem.GetComponent<CannonItem>().SetMaxStackSize(1);
            AddToStackEvent(merchantItem.GetComponent<CannonItem>());
        }
        
        GameObject merchantItem2 = Instantiate(itemsToSpawn[1], transform.position, Quaternion.identity);
        merchantItem2.transform.parent = transform;
        if (merchantItem2.GetComponent<CannonItem>() != null)
        {
            merchantItem2.GetComponent<CannonItem>().SetItemType(ItemType.Type.CANNON);
            merchantItem2.GetComponent<CannonItem>().SetMaxStackSize(1);
            AddToStackEvent(merchantItem2.GetComponent<CannonItem>());
        }
        
        GameObject merchantItem3 = Instantiate(itemsToSpawn[2], transform.position, Quaternion.identity);
        merchantItem3.transform.parent = transform;
        if (merchantItem3.GetComponent<CannonItem>() != null)
        {
            merchantItem3.GetComponent<CannonItem>().SetItemType(ItemType.Type.CANNON);
            merchantItem3.GetComponent<CannonItem>().SetMaxStackSize(1);
            AddToStackEvent(merchantItem3.GetComponent<CannonItem>());
        }
        
        GameObject merchantItem4 = Instantiate(itemsToSpawn[3], transform.position, Quaternion.identity);
        merchantItem4.transform.parent = transform;
        if (merchantItem4.GetComponent<CannonItem>() != null)
        {
            merchantItem4.GetComponent<CannonItem>().SetItemType(ItemType.Type.CANNON);
            merchantItem4.GetComponent<CannonItem>().SetMaxStackSize(1);
            AddToStackEvent(merchantItem4.GetComponent<CannonItem>());
        }

        /*foreach (var item in itemsToSpawn)
        {
            GameObject merchantItem = Instantiate(item, transform.position, Quaternion.identity);
            merchantItem.transform.parent = transform;
            merchantItem.GetComponent<Item>().SetItemType(item.GetComponent<Item>().GetItemType());
            AddToStackEvent(merchantItem.GetComponent<Item>());
        }*/
        
        /*GameObject chestItem = Instantiate(itemsToSpawn[0], transform.position, Quaternion.identity);
        chestItem.transform.parent = transform;
        if (chestItem.GetComponent<Sword>() != null)
        {
            chestItem.GetComponent<Sword>().SetItemType(ItemType.Type.SWORD);
            chestItem.GetComponent<Sword>().SetMaxStackSize(1);
            AddToStackEvent(chestItem.GetComponent<Sword>());
        }*/
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
            if (Input.GetKeyDown(KeyCode.Q) && _isOpen)
            {
                foreach (var chestSlot in merchantInventorySlots.ToList())
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
            if (merchantInventorySlots[0].GetItems().Count <= 0 &&
                merchantInventorySlots[1].GetItems().Count <= 0 &&
                merchantInventorySlots[2].GetItems().Count <= 0 &&
                merchantInventorySlots[3].GetItems().Count <= 0)
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
        
        for (var i = 0; i < merchantInventorySlots.Count; i++)
        {
            ItemStack itemStack = merchantInventorySlots[stackCounter];

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
