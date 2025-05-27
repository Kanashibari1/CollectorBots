using UnityEngine;

public class CreateBots : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Bot _bot;

    private int _minPositionZ = 20;
    private int _maxPositionZ = 30;
    private int _positionFromBase = 3;

    public Bot Create()
    {
        Bot bot = Instantiate(_bot);
        int value = Random.Range(_minPositionZ, _maxPositionZ);
        bot.transform.position = new Vector3(_spawnPosition.position.x - _positionFromBase, 0, value);
        return bot;
    }
}
