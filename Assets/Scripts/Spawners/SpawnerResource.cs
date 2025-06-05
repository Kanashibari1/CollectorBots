using System.Collections;
using UnityEngine;

public class SpawnerResource : PoolObject<Resource>
{
    [SerializeField] private Resource _resource;

    private int _minPositionX = 5;
    private int _manPositionX = 25;
    private int _minPositionZ = 5;
    private int _manPositionZ = 40;
    private int _delay = 3;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds _wait = new(_delay);

        while (enabled)
        {
           yield return _wait;

           int positionX = Random.Range(_minPositionX, _manPositionX);
           int positionZ = Random.Range(_minPositionZ, _manPositionZ);

           Resource resource = Get(_resource);
           resource.gameObject.SetActive(true);
           resource.Removed += Remove;
           resource.transform.position = new Vector3(positionX, 0, positionZ);
        }
    }

    private void Remove(Resource resource)
    {
        resource.Removed -= Remove;
        resource.gameObject.SetActive(false);
        Release(resource);
    }
}