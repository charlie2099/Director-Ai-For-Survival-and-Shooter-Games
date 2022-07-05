using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        private ItemStack _itemStackID;
        private ItemType.Type _itemType;
        private int _maxStackSize = 16;
        private string _itemInfo = "No Info";

        public virtual void UseItem()
        {
            print("Item has no use");
        }

        public void SetItemType(ItemType.Type type)
        {
            _itemType = type;
        }

        public void SetMaxStackSize(int size)
        {
            _maxStackSize = size;
        }

        public void SetItemInfo(string text)
        {
            _itemInfo = text;
        }

        public void SetItemStackID(ItemStack itemStack)
        {
            _itemStackID = itemStack;
        }

        public ItemType.Type GetItemType()
        {
            return _itemType;
        }

        public int GetMaxStackSize()
        {
            return _maxStackSize;
        }

        public string GetItemInfo()
        {
            return _itemInfo;
        }

        public ItemStack GetItemStackID()
        {
            return _itemStackID;
        }
    }
}
