using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Camera cam;
    public Transform cursor;
    private Vector2 _mousePos;

    private void Start()
    {
        Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.11f,-0.16f);
        cursor.position = _mousePos;
    }
}
