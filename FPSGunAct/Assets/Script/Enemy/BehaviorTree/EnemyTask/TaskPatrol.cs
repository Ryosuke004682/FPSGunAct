using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _wayPoints;
    private int _currentWayPointIndex = 0;

    private float _waitTime = 1.5f;
    private float _waitCount = 0;
    private bool _wating = false;

    //œpœj‚³‚¹‚é
    public TaskPatrol(Transform transform , Transform[] wayPoints)
    {
        _transform = transform;
        _wayPoints = wayPoints;
    }

    public override NodeState Request()
    {
        if(_wating)
        {
            _waitCount += Time.deltaTime;

            if(_waitCount >= _waitTime)
            {
                _wating = false;
            }
        }
        else
        {
            Transform wayPoint = _wayPoints[_currentWayPointIndex];

            if(Vector3.Distance(_transform.position, wayPoint.position) < 0.01f)
            {
                _transform.position = wayPoint.position;
                _waitCount = 0.0f;
                _wating = true;

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position , wayPoint.position , EnemysBT._speed * Time.deltaTime);

                _transform.LookAt(wayPoint.position);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }



}
