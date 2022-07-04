using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        private ItemType.Type _itemType;
        private int _maxStackSize = 16;

        public void SetItemType(ItemType.Type type)
        {
            _itemType = type;
        }

        public void SetMaxStackSize(int size)
        {
            _maxStackSize = size;
        }
        
        public ItemType.Type GetItemType()
        {
            return _itemType;
        }

        public int GetMaxStackSize()
        {
            return _maxStackSize;
        }
    }
}
