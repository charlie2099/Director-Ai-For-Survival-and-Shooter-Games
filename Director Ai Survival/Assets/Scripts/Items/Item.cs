using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        protected ItemType.Type _itemType;

        public virtual ItemType.Type GetItemType()
        {
            return _itemType;
        }
    }
}
