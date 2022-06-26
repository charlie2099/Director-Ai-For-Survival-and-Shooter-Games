using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Text uiPanelText;

    private int _health;

    private void Start()
    {
        _health = 100;
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
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        uiPanel.SetActive(true);
        uiPanelText.text = gameObject.name;
        uiPanel.transform.position = transform.position + new Vector3(0, -0.5f);
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
        // Give or drop stone to player
        Destroy(gameObject);
    }
}