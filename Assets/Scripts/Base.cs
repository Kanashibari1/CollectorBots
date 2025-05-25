using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerBots))]
[RequireComponent(typeof(Scanner))]
[RequireComponent(typeof(Storage))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _storage;

    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();
    private List<Resource> _busyResource = new();

    private Storage _scoreCounter;
    private Scanner _scanner;
    private SpawnerBots _spawnerBots;
    private Coroutine _coroutine;

    private int _botCount = 3;

    private void Awake()
    {
        _scoreCounter = GetComponent<Storage>();
        _scanner = GetComponent<Scanner>();
        _spawnerBots = GetComponent<SpawnerBots>();
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _scanner.ResourceFounded += FoundResource;
    }

    private void OnDisable()
    {
        _scanner.ResourceFounded -= FoundResource;
    }

    private void Init()
    {
        for (int i = 0; i < _botCount; i++)
        {
            Bot bot = _spawnerBots.CreateBots();
            bot.InitStartPosition(_storage);
            _freeBots.Add(bot);
        }
    }

    private IEnumerator WalkGetResource(Queue<Resource> resources)
    {
        Resource resource;

        while (resources.Count > 0)
        {
            if (_freeBots.Count > 0)
            {
                resource = resources.Dequeue();

                if (_busyResource.Contains(resource) != true)
                {
                    _busyResource.Add(resource);
                    BotStatus(_freeBots[0], resource.transform);
                }
            }

                yield return null;
        }
    }

    private void BotStatus(Bot bot, Transform target)
    {
        _freeBots.Remove(bot);
        _busyBots.Add(bot);
        bot.WalkTarget(target);
        bot.Remove += Remove;
    }

    private void Remove(Bot bot)
    {
        _freeBots.Add(bot);
        _busyBots.Remove(bot);

        if (bot._resource != null)
        {
            _busyResource.Remove(bot._resource);
            _scoreCounter.Add();
        }

        bot.Remove -= Remove;
    }

    public void FoundResource(Queue<Resource> resources)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(WalkGetResource(resources));
    }
}
