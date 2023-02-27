using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public abstract class Entry : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = SetUpTree();
        }

        protected void Update()
        {
            if(_root != null)
            {
                _root.Request();
            }
        }

        protected abstract Node SetUpTree();
    }
}