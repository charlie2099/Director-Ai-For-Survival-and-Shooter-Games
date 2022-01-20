using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        Health = enemyData.health;
    }
    
    private void Update()
    {
        base.Update();
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
}

