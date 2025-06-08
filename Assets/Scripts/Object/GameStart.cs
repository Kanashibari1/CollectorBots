using UnityEngine;

[RequireComponent(typeof(SpawnerBase))]
public class GameStart : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Storage _storage;
    [SerializeField] private BotCreates _botsCreate;

    private SpawnerBase _spawnerBase;

    private Vector3 _position = new(42,0,25);

    private void Awake()
    {
        _spawnerBase = GetComponent<SpawnerBase>();
    }

    private void Start()
    {
        Storage storage = Instantiate(_storage);
        Base @base = _spawnerBase.Spawn(_position, storage, _botsCreate);
        _botsCreate.StartGame(@base);
    }
}
