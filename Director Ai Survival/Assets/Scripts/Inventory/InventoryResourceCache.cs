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
        //private List<Item> _itemsCache = new List<Item>(); // useless

        private void Awake()
        {
            Instance = this;
        }

        public void AddToCache(Item item)
        {
            //_itemsCache.Add(item);
            ItemCollected?.Invoke(item);
        }
    }
}
