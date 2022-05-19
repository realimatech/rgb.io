using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.rgb
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private Transform _spawnPoint;

        public void SpawnProjectile()
        {
            if (_prefab != null && PoolManager.catalog.ContainsKey(_prefab.name))
            {
                Debug.Log(_spawnPoint.rotation.eulerAngles.y);
                PoolManager.catalog[_prefab.name].Spawn(_spawnPoint.position, _spawnPoint.parent.rotation);
            }
        }
    }
}
