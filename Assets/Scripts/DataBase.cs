using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Base _base;

    private List<Resource> _busyResource = new();
    private List<Resource> _foundResource = new();
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _base.Removed += Remove;
        _scanner.ResourceFounded += GetResource;
    }

    private void OnDisable()
    {
        _base.Removed -= Remove;
        _scanner.ResourceFounded -= GetResource;
    }

    public void GetResource(Queue<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            if (_busyResource.Contains(resource) != true && _foundResource.Contains(resource) != true)
            {
                _foundResource.Add(resource);
            }
        }

        StartCoroutine(_foundResource);
    }

    private void StartCoroutine(List<Resource> _foundResource)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SetTarget(_foundResource));
    }

    private IEnumerator SetTarget(List<Resource> _foundResource)
    {
        while (_foundResource.Count > 0)
        {
            Resource resource = _foundResource[0];

            if (_base.WalkGetResource(resource))
            {
                _foundResource.Remove(resource);
                _busyResource.Add(resource);
            }

            yield return null;
        }
    }

    private void Remove(Resource resource)
    {
        _busyResource.Remove(resource);
    }
}
