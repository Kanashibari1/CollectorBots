using UnityEngine;

public class BotsCreate : MonoBehaviour
{
    [SerializeField] private Bot _bot;

    private int _startCount = 3;

    private Vector3 _position = new(-3, 0, 0);

    public Bot Create(Base @base)
    {
        Bot bot = Instantiate(_bot, @base.transform.position + _position, Quaternion.identity);
        return bot;
    }

    public void StartGame(Base @base)
    {
        for (int i = 0; i < _startCount; i++)
        {
            Bot bot = Create(@base);
            @base.StartGame(bot);
        }
    }
}