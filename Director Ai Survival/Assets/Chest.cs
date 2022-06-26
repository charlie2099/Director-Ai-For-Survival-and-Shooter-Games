using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<GameObject> chestInventoryList = new List<GameObject>();
    [SerializeField] private Sprite chestOpenSprite;
    [SerializeField] private Sprite chestClosedSprite;
    [SerializeField] private GameObject chestInventoryUi;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Text uiPanelText;
    private SpriteRenderer _chestSpriteRenderer;
    private bool _isInRange;
    private bool _isOpen;
    private bool _mouseIsOver;

    private void Awake()
    {
        _chestSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        chestInventoryUi.SetActive(false);
    }

    private void Update()
    {
        if (_isInRange)
        {
            uiPanel.SetActive(true);
            uiPanel.transform.position = transform.position + new Vector3(0, -0.7f);
            uiPanelText.text = "Press SPACE";
            
            if (Input.GetKeyDown(KeyCode.Space) && !_isOpen)
            {
                _chestSpriteRenderer.sprite = chestOpenSprite;
                chestInventoryUi.SetActive(true);
                _isOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _isOpen)
            {
                _chestSpriteRenderer.sprite = chestClosedSprite;
                chestInventoryUi.SetActive(false);
                _isOpen = false;
            }
        }
        else
        {
            chestInventoryUi.SetActive(false);
            _chestSpriteRenderer.sprite = chestClosedSprite;
            
            if (!_mouseIsOver)
            {
                uiPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
    
    private void OnMouseOver()
    {
        uiPanel.SetActive(true);
        uiPanelText.text = gameObject.name;
        uiPanel.transform.position = transform.position + new Vector3(0, -0.7f);
        _mouseIsOver = true;
    }

    private void OnMouseExit()
    {
        uiPanel.SetActive(false);
        _mouseIsOver = false;
    }
}
