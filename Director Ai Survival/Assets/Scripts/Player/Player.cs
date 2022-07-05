using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Player : Entity
{
    public Action IsDead;
    public Action<int> DamageTaken;
    public Action EnergyUsed;
    [SerializeField] private SpriteRenderer sprite;

    private Item _activeItem = new Item();
    private int _maxHealth;
    private int _maxEnergy;
    private int _maxHunger;
    private int _currentEnergy;
    private int _currentHunger;

    private void Start()
    {
        Health = 100;
        _maxHealth = Health;

        _currentEnergy = 100;
        _maxEnergy = _currentEnergy;
        
        _currentHunger = 100;
        _maxHunger = _currentHunger;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetItemInHand().UseItem();
        }
    }

    protected override void Die()
    {
        base.Die();
        IsDead.Invoke();
    }

    public void ApplyHealth(int health)
    {
        if (Health <= _maxHealth)
        {
            Health += health;
        }
        else
        {
            Health = _maxHealth;
        }
    }
    
    public override void ApplyDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(PlayDamageEffect());
        DamageTaken.Invoke(damage);
    }
    
    public void UseEnergy(int energy)
    {
        _currentEnergy -= energy;
        EnergyUsed.Invoke();
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
    
    public int GetEnergy()
    {
        return _currentEnergy;
    }

    public int GetMaxEnergy()
    {
        return _maxEnergy;
    }
    
    public int GetHunger()
    {
        return _currentHunger;
    }

    public int GetMaxHunger()
    {
        return _maxHunger;
    }

    public void SetItemInHand(Item item)
    {
        _activeItem = item;
    }

    public Item GetItemInHand()
    {
        return _activeItem;
    }

    public ItemType.Type GetItemTypeInHand()
    {
        return _activeItem.GetItemType();
    }
}
