using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{ 
    protected int Health;
    protected int Damage;

    public void Update()
    {
        if (Health < 0)
        {
            Destroy(gameObject);
        }
    }
    
    public virtual void ApplyDamage(int damage) {}
    
    /*public int Health
    {
        get => _health;
        set => _health = value;
    }
    
    public void Damage(int damage)
    {
        _damage = damage;
        
    }*/
}
