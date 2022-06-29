using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Wood : Item
    {
        // TODO: Issues with using events with instantiated objects. Try Scriptable objects.

        [SerializeField] private GameObject woodObtainedText;
        
        private int _stackCounter;

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
    }
}
