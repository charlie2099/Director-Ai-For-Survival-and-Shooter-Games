using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Text uiPanelText;

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
            uiPanel.transform.position = transform.position;
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
        // Give or drop planks/wood to player
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
