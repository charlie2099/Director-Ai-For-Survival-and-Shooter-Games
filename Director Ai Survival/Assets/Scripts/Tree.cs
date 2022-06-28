using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Text uiPanelText;

    [Space]
    [SerializeField] private GameObject woodPrefab;
 
    private int _health;
    private int _maxHealth;
    private bool _inRange;

    private void Start()
    {
        _health = 100;
        _maxHealth = _health;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
            uiPanelText.text = gameObject.name;
            uiPanel.transform.position = transform.position + new Vector3(0, -1.1f);
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
            _inRange = false;
        }
    }

    private void OnMouseOver()
    {
        uiPanel.SetActive(true);
        uiPanelText.text = gameObject.name;
        uiPanel.transform.position = transform.position + new Vector3(0, -1.1f);

        if (_inRange && Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Damage");
            ApplyDamage(10);
        }
    }

    private void OnMouseExit()
    {
        uiPanel.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
    }

    private void Destroyed()
    {
        int dropLootAmount = Random.Range(1, 4);
        for (int i = 0; i < dropLootAmount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(transform.position.x - 1.0f, transform.position.x + 1.0f), 
                                            Random.Range(transform.position.y - 1.0f, transform.position.y + 1.0f));
            
            Instantiate(woodPrefab, randomPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    
    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
