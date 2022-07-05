using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Apple : Item
    {
        //public Action<int> OnConsumption;
        [SerializeField] private GameObject appleObtainedText;
        
        private int _healthGainOnConsumption = 10;
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.APPLE);
            SetItemInfo("Consume for health");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(appleObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Apple";

                InventoryResourceCache.Instance.AddToCache(this);
            }
        }

        public override void UseItem()
        {
            InventoryResourceCache.Instance.ItemToRemoveFromInv(GetItemStackID(),this);
            //print(this + " " + GetItemStackID());
            Destroy(gameObject);
            //print("Apple consumed");
            // Give player 10 health points
            // Remove from itemstack

            //OnConsumption?.Invoke(_healthGainOnConsumption);
            //InventoryResourceCache.Instance.AddToCache(this);
            //Destroy(gameObject);
            //GetComponent<Player>().ApplyHealth(_healthGainOnConsumption);
        }
    }
}