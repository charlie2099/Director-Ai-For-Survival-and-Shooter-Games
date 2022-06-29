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

        public void AddToStack(Item item) // null check?
        {
            if (itemStackList.Count <= 0)
            {
                FirstItemAddedToStack.Invoke(this, item);
            }
            itemStackList.Add(item);
            ItemStackChange.Invoke(this);
        }

        public void RemoveFromStack(Item item)
        {
            itemStackList.Remove(item);
            ItemStackChange.Invoke(this);
        }

        public int GetStackSize()
        {
            return itemStackList.Count;
        }

        public List<Item> GetItems()
        {
            return itemStackList;
        }
    }
}