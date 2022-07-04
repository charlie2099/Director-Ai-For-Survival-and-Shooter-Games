using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Sword : Item
    {
        [SerializeField] private GameObject swordObtainedText;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.SWORD);
            SetMaxStackSize(1);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(swordObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Sword";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
    }
}