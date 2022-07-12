using System.Linq;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AiDirector.AAS
{
    [RequireComponent(typeof(LineRenderer))]
    public class ActiveAreaSet : MonoBehaviour
    {
        [Header("CIRCLE PARAMETERS")]
        [SerializeField] private float radius         = 50;
        [SerializeField] private float lineWidth      = 1;
        [SerializeField] private float updateInterval = 1.0f;
    
        [Header("SPAWN CONSTRAINTS")] 
        [SerializeField] private LayerMask layerMask;

        [Header("ENEMIES")]
        [SerializeField] private float spawnInterval = 0.75f;
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private GameObject[] bosses;
        [Space] 
        [SerializeField] private GameObject[] enemyPool;
        [Space]
        [SerializeField] private GameObject enemyHierarchyContainer;
        [SerializeField] private GameObject bossHierarchyContainer;

        private LineRenderer _line;
        private AstarPath _astar;
        private GridGraph _gridGraph;
        private int segments       = 50;
        private int _randomEnemy;
        private float _timePassed;
        private float _timePassed2;
        private float _timePassed3 = 3.0f;
        private int _enemyPopulationCount;

        private void Start()
        {
            _line = gameObject.GetComponent<LineRenderer>();
            _line.positionCount = segments + 1;
            _line.widthMultiplier = lineWidth;
            _line.useWorldSpace = true;
        
            _astar = AstarPath.active;
            AstarData data = _astar.data;
            _gridGraph = data.gridGraph;
            _gridGraph.SetDimensions((int)radius*3, (int)radius*3, 0.6f);

            _timePassed = updateInterval;
            _timePassed2 = spawnInterval;
        }

        private void Update()
        {
            if (Director.Instance.GetPlayer() != null) // TODO: Tidy
            {
                DrawActiveAreaCircle();
            
                // Re-scan pathfinding grid every 1 second
                if (Time.time > _timePassed)
                {
                    _gridGraph.center = Director.Instance.GetPlayer().transform.position;
                    _astar.Scan();
                    _timePassed += updateInterval;
                }

                // Spawn enemies every 0.5 seconds
                if (Time.time > _timePassed2 && _enemyPopulationCount < Director.Instance.maxPopulationCount)
                {
                    SpawnEntity();
                    _timePassed2 += spawnInterval;

                    if (Director.Instance.directorState.CurrentTempo == DirectorState.Tempo.Peak)
                    {
                        spawnInterval /= 1.10f;
                    }
                }

                // If enemy in list is null (i.e. from being killed by player), remove from list
                for (int n = Director.Instance.activeEnemies.Count - 1; n >= 0; n--)
                {
                    if (Director.Instance.activeEnemies[n] == null)
                    {
                        Director.Instance.activeEnemies.RemoveAt(n);
                    }
                }
            
                DespawnEnemyOnAreaExit();
            }

            _enemyPopulationCount = Director.Instance.activeEnemies.Count;
        }

        private void SpawnEntity() // TODO: designer specifies layer for enemies to spawn on? 
        {
            var playerPos = Director.Instance.GetPlayer().transform.position;
            var posInSpawnRadius = playerPos + Random.insideUnitSphere * radius;
            posInSpawnRadius.z = 20;
        
            _randomEnemy = Random.Range(0, enemies.Length);
            GameObject enemy = Instantiate(enemies[_randomEnemy], posInSpawnRadius, Quaternion.identity);
            enemy.GetComponent<AIDestinationSetter>().target = Director.Instance.GetPlayer().transform;
            if (enemyHierarchyContainer != null)
            {
                enemy.transform.parent = enemyHierarchyContainer.transform;
            }
            Director.Instance.AddEnemy(enemy);

            // De-spawn enemy if they spawn too close-by to player - Not ideal...
            if (Vector2.Distance(playerPos, posInSpawnRadius) < 10)
            {
                DespawnEntity(enemy);
            }
        
        

            /*int layer = col.collider.gameObject.layer;
        if (layer == layerMask)
        {
            // spawn enemies
        }*/

            /*if (enemy.layer != layerMask) // Not ideal...
        {
            DespawnEntity(enemy);
        }*/

            /*if (LayerMask.NameToLayer(layerMask.ToString()) == 1)
        {
            // raycast downwards from enemy spawn position?
        }*/
        }

        private void DespawnEntity(GameObject entity)
        {
            Director.Instance.RemoveEnemy(entity);
            Destroy(entity);
        }
    
        private void DespawnEnemyOnAreaExit()
        {
            // If distance from player to enemy is more than the size of the radius, de-spawn enemies
            foreach (var enemy in Director.Instance.activeEnemies.ToList())
            {
                if (enemy == null) continue; //or return

                var playerPos = Director.Instance.GetPlayer().transform.position;
                var enemyPos = enemy.transform.position;

                // If distance from player to enemy is more than the size of the radius, de-spawn enemies
                if (Vector2.Distance(playerPos, enemyPos) >= radius)
                {
                    DespawnEntity(enemy);
                }
            }
        }

        public void SpawnBoss()
        {
            var playerPos = Director.Instance.GetPlayer().transform.position;
            var posInSpawnRadius = playerPos + Random.insideUnitSphere * radius;
            GameObject boss = Instantiate(bosses[0], posInSpawnRadius, Quaternion.identity);
            // Add to boss list or enemy list?
            Director.Instance.AddEnemy(boss);
        }
    
        void DrawActiveAreaCircle()
        {
            float x;
            float y;

            float angle = 20f;

            for (int i = 0; i < (segments + 1); i++)
            {
                x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + Director.Instance.GetPlayer().transform.position.x;
                y = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + Director.Instance.GetPlayer().transform.position.y;

                _line.SetPosition(i, new Vector3(x, y, 0));

                angle += (380f / segments);
            }
        }

        public int GetEnemyPopulationCount()
        {
            return _enemyPopulationCount;
        }
    }
}

