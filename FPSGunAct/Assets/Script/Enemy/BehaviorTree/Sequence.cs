using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() : base() { }
    public Sequence(List<Node> children) : base(children){ }

    public override NodeState Request()
    {
        bool anyChildIsRunning = false;

        foreach(Node node in children)
        {
            switch (node.Request())
            {
                case NodeState.FAILUDE:
                    state = NodeState.FAILUDE;
                    return state;

                case NodeState.SUCSESS:
                    continue;

                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
            }
        }

        state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCSESS;

        return state;

    }
}
