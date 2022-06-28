using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemStack : MonoBehaviour
    {
        public Action<Item> FirstItemAddedToStack;
        public Action ItemAddedToStack;
        public Action ItemRemovedFromStack;
        
        public List<Item> itemStackList = new List<Item>();

        public void AddToStack(Item item) // null check?
        {
            if (itemStackList.Count <= 0)
            {
                FirstItemAddedToStack.Invoke(item);
            }
            itemStackList.Add(item);
            ItemAddedToStack.Invoke();
        }

        public void RemoveFromStack(Item item)
        {
            itemStackList.Remove(item);
            ItemRemovedFromStack.Invoke();
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