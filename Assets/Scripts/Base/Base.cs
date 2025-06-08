using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Warehouse))]
[RequireComponent(typeof(Flag))]
[RequireComponent(typeof(StateInit))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _storagePosition;

    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();

    private Transform _position;
    private Storage _storage;
    private Warehouse _wereHouse;
    private BotCreates _botsCreate;

    public int BotsCount { get; private set; }

    public bool IsBuilding { get; private set; } = false;

    public int BuildingCount { get; private set; } = 0;

    public event Action BoiledBase;

    private void Awake()
    {
        _wereHouse = GetComponent<Warehouse>();
    }

    private void Start()
    {
        StartCoroutine(WalkGetResource());
    }

    public void Init(Storage dataBase, BotCreates botsCreate)
    {
        _storage = dataBase;
        _botsCreate = botsCreate;
    }

    public void SetTarget(Transform position)
    {
        IsBuilding = true;
        _position = position;
    }

    public void StartGame(Bot bot)
    {
        bot.GetPositionStorage(_storagePosition);
        _freeBots.Add(bot);
    }

    public void StartBuildingBase()
    {
        StartCoroutine(BuildingBase());
        _wereHouse.Remove();
    }

    public void Spawn()
    {
        Bot bot = _botsCreate.Create(this);
        bot.GetPositionStorage(_storagePosition);
        _freeBots.Add(bot);
    }

    public void AddBotNewBase(Bot bot)
    {
        _freeBots.Add(bot);
        bot.GetPositionStorage(_storagePosition);
    }

    public void RemoveBot(Bot bot)
    {
        bot.Returned -= Return;
        _busyBots.Remove(bot);
        BoiledBase.Invoke();
        bot.FinishCreated -= RemoveBot;
    }

    private IEnumerator WalkGetResource()
    {
        while (enabled)
        {
            if (_freeBots.Count > 0)
            {
                BotsCount = _freeBots.Count + _busyBots.Count;
                Resource resource = _storage.SetTarget();

                if (resource != null)
                {
                    SetBotStatus(_freeBots[0], resource.transform);
                }
            }

                yield return null;
        }
    }

    private IEnumerator BuildingBase()
    {
        while (_freeBots.Count == 0)
        {
            yield return null;
        }

        IsBuilding = false;
        BuildingCount++;
        Bot bot = _freeBots[0];
        bot.FinishCreated += RemoveBot;
        bot.Init(_storage, _botsCreate);
        SetBotStatus(bot, _position);
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
            AddResourceStorage(bot);
            _storage.Remove(bot.Resource);
        }

        bot.Returned -= Return;
    }

    private void AddResourceStorage(Bot bot)
    {
        _wereHouse.Add();
    }
}
