using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.ApplyDamage(damage);
            Destroy(gameObject);
        }

        int layer = col.collider.gameObject.layer;
        if (layer == 6)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}

