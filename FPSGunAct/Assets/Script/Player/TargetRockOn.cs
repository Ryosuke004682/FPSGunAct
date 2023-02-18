using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TargetRockOn : MonoBehaviour
{
    /*
      bool型の変数には「is～」と使ってます。
      フィールド変数には「_」←こいつ付けてます。（アンスコ）
     */

    [Header("Object")]
    [SerializeField] private Camera _mainCam;
    [SerializeField] private CinemachineFreeLook _freeLook;

    [Header("Icon")]
    [SerializeField] private Image _aimIcon;

    [Header("Setting")]
    [SerializeField] private string _enemyTag;
    [SerializeField] private Vector2 _targetLockOffset;
    [SerializeField] KeyCode selectKey;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    public bool _isTrigger;
    private float _maxAngle;
    private float _mouseX;
    private float _mouseY;

    private Transform _currentTarget;

    private void Start()
    {
        _maxAngle = 90.0f;
        _freeLook.m_XAxis.m_InputAxisName = "";
        _freeLook.m_YAxis.m_InputAxisName = "";
    }

    private void Update()
    {
        if(!_isTrigger)
        {
            _mouseX = Input.GetAxis("Mouse X");
            _mouseY = Input.GetAxis("Mouse Y");
        }
        else
        {
            NewInputTarget(_currentTarget);
        }

        if(_aimIcon)
            _aimIcon.gameObject.SetActive(_isTrigger);

        _freeLook.m_XAxis.m_InputAxisValue = _mouseX;
        _freeLook.m_YAxis.m_InputAxisValue = _mouseY;

        if (Input.GetKeyDown(selectKey))//ロックオンは「R」でする。
        {
            Debug.Log("OK");
            AssignTarget();
        }
    }

    private void AssignTarget()
    {
        if(_isTrigger)
        {
            _isTrigger = false;
            _currentTarget = null;
            return;
        }

        if(ClosestTarget())
        {
            _currentTarget = ClosestTarget().transform;
            _isTrigger = true;
        }
    }

    private void NewInputTarget(Transform target)
    {
        if (!_currentTarget)
            return;

        Vector3 viewPos = _mainCam.WorldToViewportPoint(target.position);

        if(_aimIcon)
            _aimIcon.transform.position = _mainCam.WorldToScreenPoint(target.position);

        if ((target.position - transform.position).sqrMagnitude < _minDistance)
            return;

        _mouseX = (viewPos.x - 0.5f + _targetLockOffset.x) * 3.0f;
        _mouseY = (viewPos.y - 0.5f + _targetLockOffset.y) * 3.0f;
    }


    private GameObject ClosestTarget()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(_enemyTag);
        GameObject closest = null;

        var distance = _maxDistance;
        var currAngle = _maxAngle;
        var position = transform.position;

        foreach(GameObject go in gos)
        {
            var diff = _mainCam.WorldToViewportPoint(go.transform.forward.normalized);
            var curDistance = diff.sqrMagnitude;

            if(curDistance < distance)
            {
                var viewPos = _mainCam.WorldToViewportPoint(go.transform.position);
                var newPos = new Vector3(viewPos.x - 0.5f , viewPos.y - 0.5f);

                if(Vector3.Angle(diff.normalized , _mainCam.transform.forward) < _maxAngle)
                {
                    closest = go;
                    currAngle = Vector3.Angle(diff.normalized , _mainCam.transform.forward.normalized);
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_maxDistance);
    }

}
