using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Mercenary : Entity
{
    public Action IsDead;
    [SerializeField] private SpriteRenderer sprite;

    private AIDestinationSetter _aiDestinationSetter;

    private void Awake()
    {
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        Health = 100;
        Damage = 10;
    }
    
    private void Update()
    {
        base.Update();

        if (_aiDestinationSetter.target == null)
        {
            // generate random pos nearby
        }

        // Randomly set positions nearby as the target destination (if within nav mesh)
        // If player in range, set as new target destination
    }
    
    protected override void Die()
    {
        base.Die();
        IsDead?.Invoke();
    }

    public override void ApplyDamage(float damage)
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

        if (col.gameObject.CompareTag("Enemy"))
        {
            if (col.gameObject != null || col.gameObject.GetComponent<IDamageable>() != null)
            {
                col.gameObject.GetComponent<IDamageable>().ApplyDamage(Damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _aiDestinationSetter.target = col.transform;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _aiDestinationSetter.target = null;
        }
    }
}