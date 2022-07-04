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

        private void Awake()
        {
            Instance = this;
        }

        public void AddToCache(Item item)
        {
            ItemCollected?.Invoke(item);
        }
    }
}
