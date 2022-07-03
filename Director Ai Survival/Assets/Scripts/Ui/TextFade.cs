using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        transform.parent = GameObject.Find("WorldSpaceCanvas").transform;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.color = new Color(1.0f,0.7f,0);
    }

    private void Update()
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a * 0.99f);
        if (_text.color.a <= 0.4f)
        {
            Destroy(gameObject);
        }
    }
}
