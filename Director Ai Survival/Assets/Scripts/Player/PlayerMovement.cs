using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 1;
    
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Player _player;
    private Vector2 _mousePos;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime)));

        if (_player.GetItemTypeInHand() == ItemType.Type.MUSKET)
        {
            RotateTowardsMouseCursor();
        }
        else
        {
            if (_movement.x != 0 || _movement.y != 0)
            {
                RotatePlayer();
            }
        }
    }

    private void RotatePlayer()
    {
        float angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void RotateTowardsMouseCursor()
    {
        Vector2 lookDir = _mousePos - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
    }
}