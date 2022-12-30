using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField, Header("�X�|�[����������Position")]
        private Vector3[] _spawnPosition;

        [SerializeField, Header("�X�|�[�����������I�u�W�F�N�g")]
        private GameObject[] _spawnObject;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Instantiate(_spawnObject[0], _spawnPosition[0] , Quaternion.identity);
        }
    }
}
