using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : Node
{
    private static int _enemySearchRange = 1 << 6;
    private Transform _transform;

    public EnemyChecker(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Request()  
    {
        object target = GetData("Enemy");
        if (target == null)
        {
            Collider[] collider = Physics.OverlapSphere(_transform.position ,EnemysBT._forRange , _enemySearchRange);

            if(collider.Length > 0)
            {
                entry_parent.entry_parent.SetDate("Enemy" , collider[0].transform);
                state = NodeState.SUCSESS;
                return state;
            }
            state = NodeState.FAILUDE;
            return state;
        }
        state = NodeState.SUCSESS;
        return state;
    }
}
