using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private GameObject _prefab;
    private int _poolSize;
    private Queue<T> _pool;

    public ObjectPool(in GameObject prefab, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;
        SetUpObjectPool();
    }

    private void SetUpObjectPool()
    {
        _pool = new();
        for (int i = 0; i < _poolSize; i++)
        {
            var go = Object.Instantiate(_prefab);
            _pool.Enqueue(go.GetComponent<T>());
            go.SetActive(false);
        }
    }

    public T Get()
    {
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        return default(T);
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

}
