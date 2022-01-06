using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshairCursor : MonoBehaviour
{
    public Camera cam;
    public Transform crosshair;
    
    private Rigidbody2D _rb;
    private Vector2 _mousePos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        Cursor.visible = false;
    }
    
    private void Update()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        crosshair.position = _mousePos;
    }
    
    private void FixedUpdate()
    {
        Vector2 lookDir = _mousePos - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
    }
}

