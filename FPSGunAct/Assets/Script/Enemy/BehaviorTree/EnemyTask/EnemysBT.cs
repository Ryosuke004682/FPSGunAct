using BehaviorTree;
using UnityEngine;
using System.Collections.Generic;

//BT = Behavior Tree
public class EnemysBT : Entry
{
    [SerializeField , Tooltip("?p?j?|?C???g")] public Transform[] wanderings; //wanderings = ?p?j

    public static float _speed    = 5.0f; //?ǂ????????X?s?[?h
    public static float _forRange = 6.0f; //???m?͈?
    public static float _attackRange = 0.5f; //?U???͈?

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
