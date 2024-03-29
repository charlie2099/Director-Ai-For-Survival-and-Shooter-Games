using Inventory;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Stone : Item // This should be ItemType?
    {
        [SerializeField] private GameObject stoneObtainedText;
        
        private int _stackCounter;

        private void Start()
        {
            SetItemType(ItemType.Type.STONE);
            SetItemInfo("Used to craft structures and tools");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                transform.parent = col.gameObject.transform;
                
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;

                GameObject textObject = Instantiate(stoneObtainedText, transform.position, Quaternion.identity);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = "+1 Stone";

                InventoryResourceCache.Instance.AddToCache(this);
            }
        }
    }
}