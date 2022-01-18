using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Director;

public class Generator : MonoBehaviour
{
    public static int GeneratorsOnline;

    private enum Status
    {
        Offline,
        Online
    };
    
    [SerializeField] private Status currentStatus = Status.Offline;
    [SerializeField] private float activationTime = 10.0f;
    [SerializeField] private bool inRangeOfGenerator;
    private float _timer;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    
    private void Update()
    {
        if (inRangeOfGenerator && currentStatus == Status.Offline)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            
            _timer += Time.deltaTime;
            if (_timer >= activationTime)
            {
                currentStatus = Status.Online;
                GeneratorsOnline++;
                GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inRangeOfGenerator = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && currentStatus == Status.Offline)
        {
            inRangeOfGenerator = false;
            _timer = 0; 
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

