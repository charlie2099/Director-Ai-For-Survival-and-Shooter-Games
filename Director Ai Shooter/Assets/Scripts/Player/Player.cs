using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : Entity
{
    private int _score = 0;
    private int _kills = 0;
    private int _maxHealth;
    private PlayerShooting _playerShooting;

    private void OnEnable()
    {
        EventManager.StartListening("EnemyDied", IncrementKillCount);
        EventManager.StartListening("GeneratorOnline", IncrementScore);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("EnemyDied", IncrementKillCount);
        EventManager.StopListening("GeneratorOnline", IncrementScore);
    }

    private void Awake()
    {
        _playerShooting = GetComponent<PlayerShooting>();
    }

    private void Start()
    {
        Health = 100;
        _maxHealth = Health;
    }
    
    protected override void Die()
    {
        base.Die();
        EventManager.TriggerEvent("PlayerDied", new EventParam());
    }
    
    public override void ApplyDamage(int damage)
    {
        Health -= damage;
    }

    private void IncrementKillCount(EventParam eventParam)
    {
        if (eventParam.string_ == "P1 Bullet")
        {
            _kills += 1;
            _score += 10;
        }
    }
    
    private void IncrementScore(EventParam eventParam)
    {
        _score += 100;
    }

    public int GetScore()
    {
        return _score;
    }
    
    public int GetKillCount()
    {
        return _kills;
    }

    public int GetCurrentHealth()
    {
        return this.Health;
    }
    
    public int GetMaxHealth()
    {
        return this._maxHealth;
    }

    public PlayerShooting GetWeapon()
    {
        return _playerShooting;
    }

    public void SetHealth(int amount)
    {
        Health += amount;
        if (Health == _maxHealth)
        {
            Health = _maxHealth;
        }
    }
}

