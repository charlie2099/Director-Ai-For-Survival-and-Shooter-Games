using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnLocation", menuName = "Director/SpawnLocations")]
public class SpawnLocation : ScriptableObject
{
    public Transform[] spawnLocations;
}

