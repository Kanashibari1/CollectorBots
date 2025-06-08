using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [SerializeField] private Base _base;

    public Base Spawn(Vector3 position, Storage storage, BotCreates botsCreate)
    {
        Base @base = Instantiate(_base, position, Quaternion.identity);
        @base.Init(storage, botsCreate);
        return @base;
    }
}
