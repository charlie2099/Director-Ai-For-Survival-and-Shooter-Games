using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorObjectHighlight : MonoBehaviour
{
    [SerializeField] private Sprite cursorDefaultSprite;
    [SerializeField] private Sprite cursorSelectedSprite;
    [SerializeField] private GameObject objectUiPanel;
    [SerializeField] private Text objectUiPanelText;

    private void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            GetComponent<SpriteRenderer>().sprite = cursorSelectedSprite;
            objectUiPanel.SetActive(true);
            objectUiPanelText.text = col.gameObject.name;
            objectUiPanel.transform.position = col.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        IDamageable hit = col.gameObject.GetComponent<IDamageable>();
        if (hit != null)
        {
            GetComponent<SpriteRenderer>().sprite = cursorDefaultSprite;
            objectUiPanel.SetActive(false);
        }
    }
}
