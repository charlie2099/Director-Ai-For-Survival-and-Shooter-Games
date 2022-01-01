using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;
    private Transform _player;

    private void Awake()
    {
        _player = transform;
    }

    private void Update()
    {
        var playerPos = _player.position;
        var cameraT = cam.transform;
        cameraT.position = new Vector3(playerPos.x,playerPos.y, cameraT.position.z);
    }
}

