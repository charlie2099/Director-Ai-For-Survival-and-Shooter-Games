using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public Transform cursor;

    private void Start()
    {
        Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        cursor.position = Input.mousePosition + new Vector3(12.0f, -15.06f);
    }
}
