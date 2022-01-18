using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.ApplyDamage(10);
            Destroy(gameObject);
        }
    }


    /*[SerializeField] private float speed = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        // pool bullets here?
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // pool bullets here?
        
        // set direction upon spawn
    }
    
    private void Update()
    {
        // move in direction facing when spawned
        // destroy after 5 seconds or after hitting a wall, enemy etc
        _rb.velocity = transform.right * speed;
    }*/
}

