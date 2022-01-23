using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"),LayerMask.NameToLayer("Item"));

        if (col.gameObject.CompareTag("Player"))
        {
            print("Hello");
            col.gameObject.GetComponent<Player>().SetHealth(healAmount);
            Destroy(gameObject);
        }
        
        /*Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.SetHealth(healAmount);
            
        }*/
    }
}

