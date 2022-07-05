using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryResourceCache : MonoBehaviour
    {
        public static InventoryResourceCache Instance;
        
        public Action<Item> ItemCollected;
        public Action<ItemStack, Item> ItemToRemove;

        private void Awake()
        {
            Instance = this;
        }

        public void AddToCache(Item item)
        {
            ItemCollected?.Invoke(item);
        }
        
        public void ItemToRemoveFromInv(ItemStack itemStack, Item item)
        {
            ItemToRemove?.Invoke(itemStack, item);
        }
    }
}
