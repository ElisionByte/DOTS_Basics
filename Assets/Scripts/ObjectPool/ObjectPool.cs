using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        public T prefab;
        public int poolSize;
        private Queue<T> _pool;

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                _pool.Enqueue(CreatePoolInstance());
            }
        }
        public T Get()
        {
            if (_pool.Count == 0)
                _pool.Enqueue(CreatePoolInstance());
            return GetActivatedPoolInstance();
        }
        public void ReturnToPoll(T poolInstance)
        {
            DeactivatePoolInstance(poolInstance);
            _pool.Enqueue(poolInstance);
        }
        private T CreatePoolInstance()
        {
            var poolInstance = Instantiate(prefab, this.transform);
            poolInstance.gameObject.SetActive(false);
            return poolInstance;
        }
        private T GetActivatedPoolInstance()
        {
            var poolInstance = _pool.Dequeue();
            poolInstance.gameObject.SetActive(true);
            return poolInstance;
        }
        private void DeactivatePoolInstance(T poolInstance)
        {
            var poolInstanceTransform = poolInstance.transform;
            poolInstanceTransform.SetParent(this.transform);
            poolInstanceTransform.localPosition = Vector3.zero;
            poolInstance.gameObject.SetActive(true);
        }
    }
}