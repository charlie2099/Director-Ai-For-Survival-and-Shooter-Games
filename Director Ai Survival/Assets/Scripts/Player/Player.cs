using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Items;
using UnityEngine;

public class Player : Entity
{
    public Action IsDead;
    public Action TakenDamage;
    public Action EnergyUsed;
    public Action HungerChanged;

    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _shootingSprite;

    private InventorySystem _inventorySystem;
    private SpriteRenderer _spriteRenderer;
    private Item _activeItem = new Item();
    private float _maxHealth;
    private int _maxEnergy;
    private float _maxHunger;
    private int _currentEnergy;
    private float _currentHunger;
    private int _consumablesUsed;

    private void OnEnable()
    {
        /*for (int i = 0; i < FindObjectsOfType<Apple>().Length; i++)
        {
            FindObjectsOfType<Apple>()[i].ItemConsumed += () => _consumablesUsed++;
        }*/
    }

    private void OnDisable()
    {
        /*for (int i = 0; i < FindObjectsOfType<Apple>().Length; i++)
        {
            FindObjectsOfType<Apple>()[i].ItemConsumed -= () => _consumablesUsed++;
        }*/
    }

    private void Awake()
    {
        _inventorySystem = GetComponent<InventorySystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        
        _spriteRenderer.sprite = GetItemTypeInHand() == ItemType.Type.MUSKET ? _shootingSprite : _defaultSprite;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (GetItemInHand() != null)
            {
                GetItemInHand().UseItem();
                if (GetItemTypeInHand() == ItemType.Type.APPLE)
                {
                    _consumablesUsed++;
                }
            }
        }
        
        ApplyHunger(-1.0f * Time.deltaTime);

        if (GetHunger() <= _maxHunger / 2)
        {
            ApplyDamage(5.0f * Time.deltaTime);
        }
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
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
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

    public int GetConsumablesUsed()
    {
        return _consumablesUsed;
    }

    public int GetTotalResourcesGathered()
    {
        int totalItemsInPlayerInventory = 0;
        foreach (var slot in _inventorySystem.inventorySlots)
        {
            foreach (var item in slot.itemStackList)
            {
                totalItemsInPlayerInventory++;
            }
        }
        return totalItemsInPlayerInventory;
    }
}
