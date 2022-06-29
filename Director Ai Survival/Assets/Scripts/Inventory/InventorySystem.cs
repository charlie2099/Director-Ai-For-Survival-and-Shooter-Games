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
        private int _stackCounter;

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
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                ItemStack itemStack = inventorySlots[_stackCounter];

                if (itemStack.GetStackSize() >= 1)
                {
                    _stackCounter++;
                }    
                else
                {
                    itemStack.AddToStack(item);
                    return true;
                }
            }
            return false;
        }
    }
}
