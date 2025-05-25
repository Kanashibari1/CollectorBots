using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _resourceCount;

    public event Action<int> ValueChanged;

    public void Add()
    {
        _resourceCount++;
        ValueChanged.Invoke(_resourceCount);
    }
}
