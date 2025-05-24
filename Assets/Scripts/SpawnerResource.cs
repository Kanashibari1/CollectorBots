using System.Collections;
using UnityEngine;

public class SpawnerResource : PoolObject<Resource>
{
    [SerializeField] private Resource _resource;

    private int _minPositionX = 5;
    private int _manPositionX = 25;
    private int _minPositionZ = 5;
    private int _manPositionZ = 40;
    private WaitForSeconds _wait;
    private int _delay = 2;

    private void Start()
    {
        _wait = new(_delay);
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (enabled)
        {
           int positionX = Random.Range(_minPositionX, _manPositionX);
           int positionZ = Random.Range(_minPositionZ, _manPositionZ);

           Resource resource = Get(_resource);
           resource.Removed += Remove;
           resource.transform.position = new Vector3(positionX, 0, positionZ);
           yield return _wait;
        }
    }

    private void Remove(Resource resource)
    {
        resource.Removed -= Remove;
        RemoveObj(resource);
    }
}