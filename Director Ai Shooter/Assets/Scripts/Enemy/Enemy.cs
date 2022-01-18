using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private EnemyData enemyData;

    private void Start()
    {
        Health = 100;
    }
    
    private void Update()
    {
        base.Update();
    }

    public override void ApplyDamage(int damage)
    {
        Health -= damage;
    }
}

