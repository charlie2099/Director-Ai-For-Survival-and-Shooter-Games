using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class CannonItem : Item
    {
        [SerializeField] private GameObject cannonObtainedText;
        [SerializeField] private GameObject cannon;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.CANNON);
            SetMaxStackSize(1);
            SetItemInfo("Placeable turrets");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(cannonObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Cannon";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
        
        public override void UseItem()
        {
            InventoryResourceCache.Instance.ItemToRemoveFromInv(GetItemStackID(),this);
            
            Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(cannon, objectPos, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}