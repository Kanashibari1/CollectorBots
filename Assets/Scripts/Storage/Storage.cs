using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Storage : MonoBehaviour
{
    private Scanner _scanner;

    private readonly List<Resource> _busyResource = new();
    private readonly List<Resource> _foundResource = new();

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        _scanner.ResourceFounded += GetResource;
    }

    private void OnDisable()
    {
        _scanner.ResourceFounded -= GetResource;
    }

    public void GetResource(Queue<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            if (_foundResource.Contains(resource) != true && _busyResource.Contains(resource) != true)
            {
                _foundResource.Add(resource);
            }
        }
    }

    public Resource SetTarget()
    {
        Resource resource = null;

        if (_foundResource.Count > 0)
        {
            resource = _foundResource[0];

            _busyResource.Add(resource);
            _foundResource.Remove(resource);
        }

        return resource;
    }

    public void Remove(Resource resource)
    {
        _busyResource.Remove(resource);
    }
}
