using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Director/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int damage;
    public int speed;
}

