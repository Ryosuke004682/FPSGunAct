using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Request()
        {
            foreach(Node node in children)
            {
                switch(node.Request())
                {
                    case NodeState.SUCSESS:
                        state = NodeState.SUCSESS;
                        return state;

                    case NodeState.FAILUDE:
                        continue;

                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;

                    default:
                        continue;
                }
            }

            state = NodeState.FAILUDE;
            return state;
        }

    }
}
