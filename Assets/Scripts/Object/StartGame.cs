using Unity.AI.Navigation;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Storage _storage;
    [SerializeField] private BotsCreate _botsCreate;

    private SpawnerBase _spawnerBase;

    private Vector3 _position = new(42,0,25);

    private void Awake()
    {
        _spawnerBase = GetComponent<SpawnerBase>();
    }

    void Start()
    {
        Storage storage = Instantiate(_storage);
        Base @base = _spawnerBase.Spawn(_position, storage, _botsCreate);
        _botsCreate.StartGame(@base);
    }
}
