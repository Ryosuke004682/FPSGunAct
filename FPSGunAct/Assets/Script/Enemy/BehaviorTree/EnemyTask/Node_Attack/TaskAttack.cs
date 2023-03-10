using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node
{
    private Transform _transform;
    private EnemyManager _manager;

    private float _attackTime = 1.0f;
    private float _attackCount = 0.0f;

    public TaskAttack(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Request()
    {
        Transform target = (Transform)GetData("Player");

        if(target != _transform)
        {
            Debug.Log("あいう");
            _manager = target.GetComponent<EnemyManager>();
            _transform = target;
            Debug.Log("target" + _transform);
        }

        //攻撃後のクールタイム

        _attackCount += Time.deltaTime;
        if(_attackCount >= _attackTime)
        {
            bool enemyIsDead = _manager.TaskHit();

            if(enemyIsDead)
            {
                ClearData("Enemy");
            }
            else
            {
                _attackCount = 0.0f;
            }
        }
        state = NodeState.RUNNING;
        return state;
    }

}
