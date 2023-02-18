using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RockOn : MonoBehaviour
{
    protected GameObject SetTarget()
    {
        var search_radius = 10.0f;

        //Enemyを見つける
        var hits = Physics.SphereCastAll(this.transform.position , search_radius , transform.forward , 0.01f,LayerMask.GetMask("Enemy")).Select(h => h.transform.gameObject).ToList();

        hits = FilterTargetObject(hits);


        if(0 < hits.Count())
        {
            var min_targetDistance = float.MaxValue;
            GameObject target = null;

            foreach(var hit in hits)
            {
                
                Vector3 targetScreenPoint = Camera.main.WorldToViewportPoint(hit.transform.position);

                //もっとも距離が離れてるオブジェクトを保持する。
                var target_distance = Vector2.Distance(new Vector2(0.5f,0.5f) , new Vector2(targetScreenPoint.x , targetScreenPoint.y));


                Debug.Log(hit.gameObject + ":" + target_distance);

                if(target_distance < min_targetDistance)
                {
                    min_targetDistance = target_distance;
                    target = hit.transform.gameObject;
                }
            }
            return target;
        }
        else
        {
            return null;
        }
    }

    protected List<GameObject> FilterTargetObject(List<GameObject> hits)
    {
        return hits.Where(h =>
        {
            //ここで各オブジェクトの座標を取得する。（範囲内にいる敵の）
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }).Where(h => h.tag == "Enemy").ToList();
    }


}
