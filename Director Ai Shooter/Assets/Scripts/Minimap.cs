using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemies;

    private void LateUpdate()
    {
        if (player == null) return;
        Vector3 newPos = player.position;
        transform.position = newPos;
    }
}

