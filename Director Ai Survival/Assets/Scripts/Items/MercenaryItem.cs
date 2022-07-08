using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class MercenaryItem : Item
    {
        [SerializeField] private GameObject mercenaryObtainedText;
        [SerializeField] private GameObject mercenary;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.MERCENARY);
            SetMaxStackSize(1);
            SetItemInfo("Deploys a mercenary that helps you fight off pirates");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(mercenaryObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Mercenary";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
        
        public override void UseItem()
        {
            InventoryResourceCache.Instance.ItemToRemoveFromInv(GetItemStackID(),this);
            
            Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(mercenary, objectPos, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}