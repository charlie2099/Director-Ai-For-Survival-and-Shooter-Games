using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class MusketItem : Item
    {
        [SerializeField] private GameObject musketObtainedText;
        [SerializeField] private GameObject cannonBall;

        private Transform playerFirepoint;
        
        private int _stackCounter;

        private void Awake()
        {
            playerFirepoint = GameObject.Find("PlayerFirepoint").transform;
        }

        private void Start()
        {
            SetItemType(ItemType.Type.MUSKET);
            SetMaxStackSize(1);
            SetItemInfo("Used to fight off pirates");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(musketObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Musket";
                
                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
        
        public override void UseItem()
        {
            // Instantiate bullet at the firepoint position of the player's musket
            // Fire the bullet in the direction of where the mouse was clicked
            
            /*Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = Camera.main.ScreenToWorldPoint(mousePos);*/
            GameObject bullet = Instantiate(cannonBall, playerFirepoint.position, Quaternion.identity);
            bullet.transform.parent = playerFirepoint;
        }
    }
}