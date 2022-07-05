using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Axe : Item
    {
        [SerializeField] private GameObject axeObtainedText;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.AXE);
            SetMaxStackSize(1);
            SetItemInfo("Used to chop down trees");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(axeObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Axe";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
    }
}