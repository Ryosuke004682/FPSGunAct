using BehaviorTree;
using UnityEngine;
using System.Collections.Generic;

//BT = Behavior Tree
public class EnemysBT : Entry
{
    public Transform[] wanderings; //wanderings = œpœj

    public static float _speed    = 5.0f;
    public static float _forRange = 6.0f;
    public static float _attackRange = 0.5f;

    protected override Node SetUpTree()
    {
        Node node = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new EnemyAttackRange(transform),
                new TaskAttack(transform)
            }),
            new Sequence(new List<Node>
            {
                new EnemyChecker(transform),
                new EnemyTask(transform)
            }),
            new TaskPatrol(transform , wanderings)
        });

        return node;
    }

}
