using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{ 
    protected float Health;
    protected int Damage;

    public virtual void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public virtual void ApplyDamage(float damage) {}

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
