using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new();

    public T Get(T prefab)
    {
        if(_pool.Count == 0)
        {
            Create(prefab);
        }
        
        T obj = _pool.Dequeue();

        return obj;
    }

    public void Release(T obj)
    {
        _pool.Enqueue(obj);
    }

    private void Create(T prefab)
    {
        T @object = GameObject.Instantiate(prefab);
        _pool.Enqueue(@object);
    }
}
