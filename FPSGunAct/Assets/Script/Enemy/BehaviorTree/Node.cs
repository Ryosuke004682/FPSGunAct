using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //成功
    //失敗
    //実行中
    public enum NodeState
    {
        SUCSESS,
        FAILUDE,
        RUNNING
    }
   
    public class Node
    {
        protected NodeState state;
        public Node entry_parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = 
                             new Dictionary<string, object>();


        public Node()
        {
            entry_parent = null;
        }
        public Node(List<Node> child)
        {
            foreach(var children in child)
            {
                Attack(children);
            }
        }

        private void Attack(Node node)
        {
            node.entry_parent = this;
            children.Add(node);
        }

        //各派生ノード空のリクエストを受けるための関数を作成する。
        public virtual NodeState Request() => NodeState.FAILUDE;


        public void SetDate(string key , object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key , out value))
            {
                return value;
            }

            Node node = entry_parent;

            while(node != null)
            {
                value = node.GetData(key);

                if(value != null)
                {
                    return value;
                }
                node = node.entry_parent;
            }

            return null;
        }

        public bool ClearData(string tagName)
        {
            if(_dataContext.ContainsKey(tagName))
            {
                _dataContext.Remove(tagName);
                return true;

            }

            Node node = entry_parent;
            while(node != null)
            {
                bool cleared = node.ClearData(tagName);

                if(cleared)
                {
                    return true;
                }
                node = node.entry_parent;
            }
            return false;
        }
    }
}
