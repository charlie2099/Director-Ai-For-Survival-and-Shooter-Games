using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemStack : MonoBehaviour
    {
        public Action<ItemStack, Item> FirstItemAddedToStack;
        public Action<ItemStack> ItemStackChange;
        public List<Item> itemStackList = new List<Item>();

        private ItemType.Type _stackItemType = ItemType.Type.NONE;
        private int _maxStackSize = 16;

        public void AddToStack(Item item) 
        {
            if (itemStackList.Count <= 0)
            {
                FirstItemAddedToStack?.Invoke(this, item);
            }
            itemStackList.Add(item);
            ItemStackChange?.Invoke(this);
            item.SetItemStackID(this);
        }

        public void RemoveFromStack(Item item)
        {
            //print("Received Item To Remove: " + item.name);
            itemStackList.Remove(item);
            ItemStackChange?.Invoke(this);
        }

        public void SetStackItemType(ItemType.Type itemType)
        {
            _stackItemType = itemType;
        }

        public void SetMaxStackSize(int stackSize)
        {
            _maxStackSize = stackSize;
        }

        public ItemType.Type GetItemType()
        {
            return _stackItemType;
        }

        public int GetCurrentStackSize()
        {
            return itemStackList.Count;
        }

        public int GetMaxStackSize()
        {
            return _maxStackSize;
        }

        public List<Item> GetItems()
        {
            return itemStackList;
        }
    }
}