using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
{
    private Queue<T> _pool = new();

    public T Get(T @object)
    {
        if(_pool.Count == 0)
        {
            Create(@object);
        }
        
        T obj = _pool.Dequeue();

        return obj;
    }

    public void Release(T obj)
    {
        _pool.Enqueue(obj);
    }

    private void Create(T obj)
    {
        T @object = GameObject.Instantiate(obj);
        _pool.Enqueue(@object);
    }
}
