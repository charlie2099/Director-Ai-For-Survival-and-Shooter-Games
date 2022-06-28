using System;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Wood : Item
    {
        [SerializeField] private GameObject woodObtainedText;
        private Inventory _playerInventory;

        private void Awake()
        {
            _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                //Destroy(GetComponent<SpriteRenderer>());
                //Destroy(GetComponent<BoxCollider2D>());
                
                GameObject textObject = Instantiate(woodObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Wood";
                _playerInventory.inventorySlots[0].AddToStack(this);
            }
        }
    }
}
