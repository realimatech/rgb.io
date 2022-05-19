using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace realima.rgb
{
    public class PoolManager : MonoBehaviour
    {
        public static Dictionary<string, PoolManager> catalog = new Dictionary<string, PoolManager>();

        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _initialBuffer;
        [SerializeField]
        private Transform _parent;

        private Queue<GameObject> _pool = new Queue<GameObject>();

        private void OnEnable()
        {
            var key = _prefab.name;
            if (catalog.ContainsKey(key))
            {
                Destroy(catalog[key].gameObject);
                catalog.Remove(key);
            }
            catalog.Add(key, this);

            StartBuffer();
        }

        private void OnDisable()
        {
            catalog.Remove(_prefab.name);
        }

        private void StartBuffer()
        {
            AddInstances(_initialBuffer);
        }

        private void AddInstances(int amount)
        {
            if (_prefab.GetComponent<ISpawnable>() == null) { Debug.LogError("Unexpected GameObject! Doesn't contain ISpawnable component"); return; }
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(_prefab, _parent);
                go.SetActive(false);
                _pool.Enqueue(go);
            }
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if (_pool.Count == 0) AddInstances(5);
            GameObject spawn = _pool.Dequeue();

            spawn.transform.SetPositionAndRotation(position, rotation);
            Debug.Log(spawn.transform.eulerAngles);
            spawn.SetActive(true);

            var spawnable = spawn.GetComponent<ISpawnable>();
            if(spawnable != null)
                spawnable.despawnEvent += OnDespawn;
        }

        private void OnDespawn(GameObject spawn)
        {
            _pool.Enqueue(spawn);
            spawn.SetActive(false);
        }
    }
}
