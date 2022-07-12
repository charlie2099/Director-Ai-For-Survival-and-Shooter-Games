using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    //[SerializeField] private int refillAmount = 20;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"),LayerMask.NameToLayer("Item"));

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().GetWeapon().RefillAmmo();
            Destroy(gameObject);
        }
    }
}
