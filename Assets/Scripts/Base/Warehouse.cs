using System;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    public int ResourceCount { get; private set; }

    public event Action<int> ValueChanged;

    public void Add()
    {
        ResourceCount++;
        ValueChanged?.Invoke(ResourceCount);
    }

    public void Remove(int resourceCount)
    {
        ResourceCount -= resourceCount;
        ValueChanged?.Invoke(ResourceCount);
    }
}
