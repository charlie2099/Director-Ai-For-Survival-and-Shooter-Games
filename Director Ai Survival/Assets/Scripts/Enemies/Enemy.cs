using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //[SerializeField] private EnemyData enemyData;
    [SerializeField] private SpriteRenderer sprite;

    //private EventParam _eventParam;

    private void Start()
    {
        //Health = enemyData.health;
        Health = 100;
        Damage = 10;
    }
    
    private void Update()
    {
        base.Update();
    }
    
    protected override void Die()
    {
        base.Die();
        //EventManager.TriggerEvent("EnemyDied", _eventParam);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "P1 Bullet")
        {
            //_eventParam.string_ = col.gameObject.name;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject != null || col.gameObject.GetComponent<IDamageable>() != null)
            {
                col.gameObject.GetComponent<IDamageable>().ApplyDamage(Damage);
            }
        }
    }
}