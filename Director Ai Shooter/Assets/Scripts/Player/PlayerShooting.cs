using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //[SerializeField] private GameObject weaponType;
    //[SerializeField] private Weapon weaponType;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int damage;
    [SerializeField] private int fireRate;
    [SerializeField] private int ammoClipSize;

    [SerializeField] private float bulletForce = 20.0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        EventParam eventParam = new EventParam(); eventParam.soundstr_ = "GunShot";
        EventManager.TriggerEvent("GunFired", eventParam);
    }
}

