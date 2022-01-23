using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceFromEnemy : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float intensityChange;

    private void Update()
    {
        CheckDistanceFromPlayer();
    }

    private void CheckDistanceFromPlayer()
    {
        if (Director.Instance.GetPlayer() != null && Director.Instance.GetTempo() != Director.Tempo.PeakFade)
        {
            Vector2 playerPos = Director.Instance.GetPlayer().transform.position;

            foreach (var enemy in Director.Instance.activeEnemies)
            {
                if (enemy != null)
                {
                    Vector2 enemyPos = enemy.transform.position;
                    if (Vector2.Distance(playerPos, enemyPos) < distance) 
                    {
                        Director.Instance.IncreaseIntensity(intensityChange);
                    }
                }
            }
        }
    }
}

