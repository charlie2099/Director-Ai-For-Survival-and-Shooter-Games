using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Wood : Item // This should be ItemType?
    {
        [SerializeField] private GameObject woodObtainedText;
        
        private int _stackCounter;

        private void Start()
        {
            //_itemType = ItemType.Type.WOOD;
            SetItemType(ItemType.Type.WOOD);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(woodObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Wood";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }

        /*public override ItemType.Type GetItemType()
        {
            return _itemType;
        }*/
    }
}
