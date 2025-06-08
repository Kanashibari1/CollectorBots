using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Removed;

    public void Remove()
    {
        Removed.Invoke(this);
    }
}
