using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Player : Entity
{
    public Action IsDead;
    public Action TakenDamage;
    public Action EnergyUsed;
    public Action HungerChanged;
    [SerializeField] private SpriteRenderer sprite;

    private Item _activeItem = new Item();
    private float _maxHealth;
    private int _maxEnergy;
    private float _maxHunger;
    private int _currentEnergy;
    private float _currentHunger;

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
            if (GetItemInHand() != null)
            {
                GetItemInHand().UseItem();
            }
        }
        
        ApplyHunger(-1.0f * Time.deltaTime);

        if (GetHunger() <= _maxHunger / 2)
        {
            ApplyDamage(5.0f * Time.deltaTime);
        }
        
        
        //print("Item in hand after use: " + GetItemInHand());
    }

    protected override void Die()
    {
        base.Die();
        IsDead?.Invoke();
    }

    public void ApplyHealth(int health)
    {
        if (Health <= _maxHealth)
        {
            Health += health;
            TakenDamage?.Invoke();
        }
        else
        {
            Health = _maxHealth;
        }
    }
    
    public override void ApplyDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(PlayDamageEffect());
        TakenDamage?.Invoke();
    }

    public void ApplyHunger(float hunger)
    {
        if (_currentHunger <= _maxHunger)
        {
            _currentHunger += hunger;
            HungerChanged?.Invoke();
        }
        else
        {
            _currentHunger = _maxHunger;
        }
    }
    
    public void UseEnergy(int energy)
    {
        _currentEnergy -= energy;
        EnergyUsed?.Invoke();
    }
    
    private IEnumerator PlayDamageEffect()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetMaxHealth()
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
    
    public float GetHunger()
    {
        return _currentHunger;
    }

    public float GetMaxHunger()
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
