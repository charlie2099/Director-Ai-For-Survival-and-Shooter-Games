using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                
                EventParam soundEvent1 = new EventParam(); soundEvent1.soundstr_ = "GeneratorPoweringUp";
                EventManager.TriggerEvent("GeneratorStopped", soundEvent1);
                
                EventParam soundEvent2 = new EventParam(); soundEvent2.soundstr_ = "GeneratorActivated";
                EventManager.TriggerEvent("GeneratorOnline", soundEvent2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && currentStatus == Status.Offline)
        {
            inRangeOfGenerator = true;
            
            EventParam soundEvent3 = new EventParam(); soundEvent3.soundstr_ = "GeneratorPoweringUp";
            EventManager.TriggerEvent("GeneratorActivating", soundEvent3);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && currentStatus == Status.Offline)
        {
            inRangeOfGenerator = false;
            _timer = 0; 
            GetComponent<SpriteRenderer>().color = Color.red;
            
            EventParam soundEvent1 = new EventParam(); soundEvent1.soundstr_ = "GeneratorPoweringUp";
            EventManager.TriggerEvent("GeneratorStopped", soundEvent1);
        }
    }
}

