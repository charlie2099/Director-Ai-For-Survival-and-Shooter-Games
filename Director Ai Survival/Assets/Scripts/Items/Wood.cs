using System;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Wood : Item
    {
        public Action<Item> ItemCollected;
        
        [SerializeField] private GameObject woodObtainedText;
        
        private Inventory _playerInventory;
        private int stackCounter;

        private void Awake()
        {
            _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                //Destroy(GetComponent<SpriteRenderer>());
                //Destroy(GetComponent<BoxCollider2D>());
                
                GameObject textObject = Instantiate(woodObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Wood";

                ItemCollected?.Invoke(this);


                // STACK COUNTER ONLY EXISTS ON EACH INDIVIDUAL WOOD PIECE!
                // SO IT WILL ONLY EXECUTE THIS ONCE, BEFORE IT IS THEN DESTROYED!
                if (_playerInventory.inventorySlots[0].GetStackSize() < 3)
                {
                    _playerInventory.inventorySlots[0].AddToStack(this);
                }
                else
                {
                    _playerInventory.inventorySlots[1].AddToStack(this);
                }
            }
        }
    }
}
