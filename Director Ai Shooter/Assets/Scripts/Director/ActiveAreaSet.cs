using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Random = UnityEngine.Random;

//[RequireComponent(typeof(CircleCollider2D))]
//[RequireComponent(typeof(LineRenderer))]
public class ActiveAreaSet : MonoBehaviour
{
    public static int EnemyPopulationCount;
    
    [Header("Circle Parameters")]
    [SerializeField] private float radius = 50;
    [SerializeField] private int segments = 50;
    [SerializeField] private float lineWidth = 1;
    
    [Header("Enemies To Populate")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] bosses;
    
    [Header("Constraints")]
    [SerializeField] private int maxPopulationCount;
    [Range(1, 10)]
    [SerializeField] private int spawnFrequency;

    private List<GameObject> _enemyContainer = new List<GameObject>();

    private Director _director;
    private LineRenderer _line;
    private AstarPath _astar;
    private Pathfinding.GridGraph _gridGraph;

    private float _timePassed  = 1.0f;
    private float _timePassed2 = 0.5f;
    private float _timePassed3 = 3.0f;

    // Determines which enemies are bosses through Boss tag? Means designer will need to create
    // and apply this tag to all their bosses (prefabs).

    private void Awake() {}

    private void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.positionCount = segments + 1;
        _line.widthMultiplier = lineWidth;
        _line.useWorldSpace = true;
        
        _astar = AstarPath.active;
        Pathfinding.AstarData data = _astar.data;
        _gridGraph = data.gridGraph;
        _gridGraph.SetDimensions((int)radius*3, (int)radius*3, 0.6f);
    }

    private void Update()
    {
        DrawActiveAreaCircle();

        if (Time.time > _timePassed)
        {
            _gridGraph.center = Director.Instance.GetPlayer().transform.position;
            _astar.Scan();
            _timePassed += 1.0f;
        }

        if (Time.time > _timePassed2)
        {
            SpawnEntity();
            _timePassed2 += 0.5f;
        }

        Vector3 playerPos = Director.Instance.GetPlayer().transform.position;
        //Vector3 enemyPos = enemies[0].transform.position;
        
        // if distance from player to enemy is more than the size of the radius, despawn enemies
        foreach (var enemy in _enemyContainer)
        {
            if (enemy != null)
            {
                if(Vector3.Distance(playerPos, enemy.transform.position) >= radius)
                {
                    DespawnEntity(enemy);
                }
            }
        }
    }

    private void SpawnEntity()
    {
        // Spawn enemy in a random position inside active area
        GameObject enemy = Instantiate(enemies[0], Director.Instance.GetPlayer().transform.position + Random.insideUnitSphere * radius, Quaternion.identity);
        _enemyContainer.Add(enemy);
    }

    private void DespawnEntity(GameObject entity)
    {
        Destroy(entity);
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
}

