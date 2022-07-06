using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject muzzleFlash;
    private Transform _target;

    private float timePassed = 3.0f;

    private void Update()
    {
        if (_target != null)
        {
            RotateTowardsTarget();

            if (Time.time > timePassed)
            {
                Fire();
                timePassed = Time.time + 3.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _target = col.transform;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _target = null;
        }
    }
    
    private void Fire()
    {
        GameObject bullet = Instantiate(cannonBall, firePoint.position, Quaternion.identity);
        //bullet.transform.parent = projectileContainer.transform;
        //bullet.name = "P1 Bullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * 20.0f, ForceMode2D.Impulse);

        //EventParam eventParam = new EventParam(); eventParam.soundstr_ = "GunShot";
        //EventManager.TriggerEvent("GunFired", eventParam);

        StartCoroutine(PlayMuzzleFlashEffect());
    }

    private IEnumerator PlayMuzzleFlashEffect()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    private void RotateTowardsTarget()
    {
        var offset = 0f;
        Vector2 direction = _target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
