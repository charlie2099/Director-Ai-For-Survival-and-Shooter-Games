using UnityEngine;

namespace Director
{
    public class Director : MonoBehaviour
    {
        private enum Phase
        {
            RESPITE,
            BUILD_UP,
            PEAK
        }

        [Header("Attributes?")]
        [SerializeField] private float perceivedIntensity;
        [SerializeField] private int activeEntities;
        [SerializeField] private int maxItemSpawns;
        [SerializeField] private int maxEnemySpawns;
        [SerializeField] private Phase activeIntensityPhase;
    
        [Header("Data")]
        //[SerializeField] private PlayerData[] players;
        //[SerializeField] private EnemyData[] enemies;
        //[SerializeField] private ItemData[] items;
        [SerializeField] private GameObject[] players;
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private GameObject[] items;
    
        [Header("Entity Spawn Locations")]
        [Tooltip("All the possible spawn locations for an entity such as items, powerups, weapons etc. Enemies however are spawned with the ActiveAreaSet so do not include them here.")]
        [SerializeField] private GameObject[] spawnpointContainers;
    }
}

