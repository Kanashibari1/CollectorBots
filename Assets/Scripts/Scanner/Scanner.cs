using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    private int _delay = 5;
    private int _radius = 500;
    private Queue<Resource> resourcesOnMap = new();

    public event Action<Queue<Resource>> ResourceFounded;

    private void Start()
    {
        StartCoroutine(ScanCoroutine());
    }

    private void FindRecourseOnMap()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _mask);

        resourcesOnMap.Clear();

        foreach (Collider collider in colliders)
        {
            collider.TryGetComponent(out Resource resource);

            resourcesOnMap.Enqueue(resource);
        }
    }

    private IEnumerator ScanCoroutine()
    {
        WaitForSeconds _sleepTime = new(_delay);

        while (enabled)
        {
            yield return _sleepTime;

            FindRecourseOnMap();
            ResourceFounded.Invoke(resourcesOnMap);
        }
    }
}
