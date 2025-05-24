using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Removed;

    public bool IsTaken { get; private set; } = false;

    public void IsTake()
    {
        IsTaken = true;
    }

    public void Remove()
    {
        Removed.Invoke(this);
    }
}
