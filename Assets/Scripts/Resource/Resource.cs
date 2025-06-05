using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Removed;

    public bool IsTake { get; private set; }

    public void Remove()
    {
        Removed.Invoke(this);
    }
}
