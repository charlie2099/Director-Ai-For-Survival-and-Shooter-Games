using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class EnemyPathfinding : MonoBehaviour
{
    public Transform target;
    public float speed                = 200.0F;
    public float nextWaypointDistance = 3.0F;

    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private int _currentWaypoint   = 0;
    private bool _reachedEndOfPath = false;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0, 0.5F);
    }
    
    void UpdatePath()
    {
        if(_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_path == null)
            return;

        if(_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        } 
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }
}

