using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Vector3 _position = new(0, 1, 0);

    public Base Spawn(Vector3 position, Storage storage, BotsCreate botsCreate)
    {
        Base @base = Instantiate(_base, position + _position, Quaternion.identity);
        @base.init(storage, botsCreate);
        return @base;
    }
}
