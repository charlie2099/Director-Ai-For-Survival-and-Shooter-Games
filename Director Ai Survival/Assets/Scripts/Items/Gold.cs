using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Gold : Item 
    {
        [SerializeField] private GameObject goldObtainedText;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.GOLD);
            SetItemInfo("Used to buy things from merchants");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(goldObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Gold";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
    }
}