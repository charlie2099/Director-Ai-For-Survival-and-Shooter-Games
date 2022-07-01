using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Image inventoryBackpackIcon;
        [SerializeField] private Sprite inventoryBackpackOpenSprite;
        [SerializeField] private Sprite inventoryBackpackClosedSprite;
    
        public List<ItemStack> inventorySlots = new List<ItemStack>();
        
        private bool _inventoryIsOpen;
        private const int STACK_SIZE = 16;

        private void OnEnable()
        {
            foreach (var slot in inventorySlots)
            {
                slot.FirstItemAddedToStack += SetInventorySlotImage;
                slot.ItemStackChange += UpdateStackSize;
            }

            InventoryResourceCache.Instance.ItemCollected += AddToStackEvent;
        }

        private void OnDisable()
        {
            foreach (var slot in inventorySlots)
            {
                slot.FirstItemAddedToStack -= SetInventorySlotImage;
                slot.ItemStackChange -= UpdateStackSize;
            }
            
            InventoryResourceCache.Instance.ItemCollected -= AddToStackEvent;
        }

        private void Start()
        {
            inventoryPanel.SetActive(false);

            foreach (var slot in inventorySlots)
            {
                slot.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = slot.GetStackSize().ToString();
            }
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
            print("<color=green>" + itemStack.transform.name + "</color>, <color=cyan>Stack size: </color>" + itemStack.GetStackSize());
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
            
            for (var i = 0; i < inventorySlots.Count; i++)
            {
                ItemStack itemStack = inventorySlots[stackCounter];

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
}
