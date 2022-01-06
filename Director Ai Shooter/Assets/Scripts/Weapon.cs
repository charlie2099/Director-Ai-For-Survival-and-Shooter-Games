using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponType;
    [SerializeField] private GameObject projectileType;
    [SerializeField] private int damage;
    [SerializeField] private int fireRate;
    [SerializeField] private int ammoClipSize;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(projectileType, transform.position, Quaternion.identity);
    }
}

