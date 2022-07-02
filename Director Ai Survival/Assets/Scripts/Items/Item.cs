using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        private ItemType.Type _itemType;

        public void SetItemType(ItemType.Type type)
        {
            _itemType = type;
        }
        
        public ItemType.Type GetItemType()
        {
            return _itemType;
        }
    }
}
