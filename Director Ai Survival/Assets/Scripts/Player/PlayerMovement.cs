using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 1;
    private Rigidbody2D _rb;
    private Vector2 _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime)));

        if (_movement.x != 0 || _movement.y != 0)
        {
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        float angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}