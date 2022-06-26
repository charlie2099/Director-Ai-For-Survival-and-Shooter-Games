using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private SpriteRenderer sprite;
    private int _maxHealth;

    private void Start()
    {
        Health = 100;
        _maxHealth = Health;
    }

    protected override void Die()
    {
        base.Die();
        //EventManager.TriggerEvent("PlayerDied", new EventParam());
    }
    
    public override void ApplyDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(PlayDamageEffect());
    }
    
    private IEnumerator PlayDamageEffect()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    public int GetHealth()
    {
        return Health;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
