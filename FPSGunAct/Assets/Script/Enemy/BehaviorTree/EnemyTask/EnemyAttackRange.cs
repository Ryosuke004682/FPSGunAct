using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : Node
{
    private Transform _transform;

    public EnemyAttackRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Request()
    {
        object enemy = GetData("Enemy");
        
        if(enemy == null)
        {
            state = NodeState.FAILUDE;
            return state;
        }

        Transform target = (Transform)enemy;
        if(Vector3.Distance(_transform.position , target.position) <= EnemysBT._attackRange)
        {
            state = NodeState.SUCSESS;
            return state;
        }
        state = NodeState.SUCSESS;
        return state;
    }
}
