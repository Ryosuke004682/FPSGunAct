using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnCursor : MonoBehaviour
{
    protected RectTransform rct; 
    protected Image image;

    protected Transform LockOnTarget { get; set; }

    private void Start()
    {
        rct = this.GetComponent<RectTransform>();
        image.enabled = false;
    }

    private void Update()
    {
        if(image.enabled)
        {
            rct.Rotate(0,0,1f); //rct = RectTransform

            if (LockOnTarget != null)
            {
                Vector3 targetPoint = Camera.main.WorldToScreenPoint(LockOnTarget.position);
                rct.position = targetPoint; 
            }
        }
    }

    public void OnLockOnStart(Transform target)
    {
        image.enabled = true;
        LockOnTarget = target;
    }

    public void OnLockOnEnd()
    {
        image.enabled = false;
        LockOnTarget = null;
    }
}
