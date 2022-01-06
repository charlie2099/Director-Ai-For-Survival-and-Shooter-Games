using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//[RequireComponent(typeof(CircleCollider2D))]
public class ActiveAreaSet : MonoBehaviour
{
    public static int enemyPopulationCount;
    
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] bosses;

    [SerializeField] private int maxPopulationCount;
    [SerializeField] private int activeAreaSize; 
    
    [Range(1, 10)]
    [SerializeField] private int spawnFrequency;

    // Determines which enemies are bosses through Boss tag? Means designer will need to create
    // and apply this tag to all their bosses (prefabs).
}

