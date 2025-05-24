using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Bot _bot;

    public Bot CreateBots()
    {
        Bot bot = Instantiate(_bot);
        int value = Random.Range(20, 30);
        bot.transform.position = new Vector3(_spawnPosition.position.x - 3, 0, value);
        return bot;
    }
}
