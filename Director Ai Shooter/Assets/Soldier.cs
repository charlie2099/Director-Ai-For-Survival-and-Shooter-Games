using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    [SerializeField] private Transform player;

    private void Awake()
    {
        Health = 100;
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }
}

