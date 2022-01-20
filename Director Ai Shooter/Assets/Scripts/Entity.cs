using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{ 
    protected int Health;
    protected int Damage;

    public void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public virtual void ApplyDamage(int damage) {}

    private void Die()
    {
        Destroy(gameObject);
    }
}
