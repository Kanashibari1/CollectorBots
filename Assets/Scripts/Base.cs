using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreateBots))]
[RequireComponent(typeof(Scanner))]
[RequireComponent(typeof(Storage))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _storage;

    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();

    private Storage _scoreCounter;
    private CreateBots _spawnerBots;

    private int _botCount = 3;

    public event Action<Resource> Removed;

    private void Awake()
    {
        _scoreCounter = GetComponent<Storage>();
        _spawnerBots = GetComponent<CreateBots>();
    }

    private void Start()
    {
        Init();
    }

    public bool WalkGetResource(Resource resource)
    {
        if (_freeBots.Count > 0)
        {
            SetBotStatus(_freeBots[0], resource.transform);
            return true;
        }

        return false;
    }

    private void Init()
    {
        for (int i = 0; i < _botCount; i++)
        {
            Bot bot = _spawnerBots.Create();
            bot.InitStartPosition(_storage);
            _freeBots.Add(bot);
        }
    }

    private void SetBotStatus(Bot bot, Transform target)
    {
        _freeBots.Remove(bot);
        _busyBots.Add(bot);
        bot.WalkTarget(target);
        bot.Returned += Return;
    }

    private void Return(Bot bot)
    {
        _freeBots.Add(bot);
        _busyBots.Remove(bot);

        if (bot.Resource != null)
        {
            Removed.Invoke(bot.Resource);
            _scoreCounter.Add();
        }

        bot.Returned -= Return;
    }
}
