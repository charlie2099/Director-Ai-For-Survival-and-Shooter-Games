using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectileContainer;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Transform firePoint;
    //[SerializeField] private int damage;
    //[SerializeField] private int fireRate;
    [Space]
    [SerializeField] private int ammoMagCapacity;
    [SerializeField] private int numOfAmmoMags;
    [SerializeField] private float bulletForce = 20.0f;
    
    private int _currentBulletCount;
    private int _currentMag;
    private float _reloadTimer;
    private float _reloadTime;

    private void Start()
    {
        _currentBulletCount = ammoMagCapacity;
        _currentMag = numOfAmmoMags;
    }

    private void Update()
    {
        ammoText.text = _currentBulletCount + " / " + _currentMag;
        
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        
        if (_reloadTimer > 0)
        {
            _reloadTimer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (_currentBulletCount > 0 && _currentMag > 0)
        {
            if (_reloadTimer <= 0)
            {
                GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
                bullet.transform.parent = projectileContainer.transform;
                bullet.name = "P1 Bullet";
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

                EventParam eventParam = new EventParam(); eventParam.soundstr_ = "GunShot";
                EventManager.TriggerEvent("GunFired", eventParam);
            
                _currentBulletCount--;
                _reloadTimer = 0.20f;
                if (_currentBulletCount <= 0)
                {
                    _currentMag--;
                    _reloadTimer = 2;

                    if (_currentMag == 0)
                    {
                        return;
                    }
                    _currentBulletCount = ammoMagCapacity;
                }

                StartCoroutine(PlayMuzzleFlashEffect());
            }
        }
        
        print("Bullets left: " + _currentBulletCount);
        print("Mags left: " + _currentMag);
    }

    private IEnumerator PlayMuzzleFlashEffect()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    public void SetAmmo(int refillAmount)
    {
        _currentBulletCount += refillAmount;

        if (_currentBulletCount > ammoMagCapacity)
        {
            _currentMag++;
            //_currentBulletCount -= refillAmount;
        }
    }

    public void RefillAmmo()
    {
        _currentBulletCount = 10;
    }
}

