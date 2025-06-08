using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Warehouse))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _storagePosition;

    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();

    private Transform _positionNewBase;
    private Storage _storage;
    private Warehouse _wereHouse;
    private BotCreates _botsCreate;

    private int _resourceCreateBots = 3;
    private int _resourceCreateBase = 5;

    public int BotsCount => _freeBots.Count + _busyBots.Count;

    public bool IsBuilding { get; private set; } = false;

    public int BuildingCount { get; private set; } = 0;

    public event Action BuiltBase;

    private void Awake()
    {
        _wereHouse = GetComponent<Warehouse>();
    }

    private void Start()
    {
        StartCoroutine(AssignTaskBot());
    }

    public void Init(Storage dataBase, BotCreates botsCreate)
    {
        _storage = dataBase;
        _botsCreate = botsCreate;
    }

    public void SetTarget(Transform position)
    {
        IsBuilding = true;
        _positionNewBase = position;
    }

    public void StartGame(Bot bot)
    {
        bot.SetPositionStorage(_storagePosition);
        _freeBots.Add(bot);
    }

    public void SpawnBots()
    {
        Bot bot = _botsCreate.Create(this);
        bot.SetPositionStorage(_storagePosition);
        _freeBots.Add(bot);
    }

    public void AddBotNewBase(Bot bot)
    {
        _freeBots.Add(bot);
        bot.SetPositionStorage(_storagePosition);
    }

    public void RemoveBot(Bot bot)
    {
        bot.Returned -= Return;
        _busyBots.Remove(bot);
        BuiltBase.Invoke();
        bot.FinishCreated -= RemoveBot;
    }

    private IEnumerator AssignTaskBot()
    {
        while (enabled)
        {
            if (_freeBots.Count > 0)
            {
                Resource resource = _storage.GetTarget();

                if (resource != null)
                {
                    SetBotTask(_freeBots[0], resource.transform);
                }
            }

                yield return null;
        }
    }

    private void CreateBase()
    {
        IsBuilding = false;
        BuildingCount++;
        Bot bot = _freeBots[0];
        bot.FinishCreated += RemoveBot;
        bot.Init(_storage, _botsCreate);
        SetBotTask(bot, _positionNewBase);
    }

    private void SetBotTask(Bot bot, Transform target)
    {
        _freeBots.Remove(bot);
        _busyBots.Add(bot);
        bot.SetTarget(target);
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

        State();
    }

    private void AddResourceStorage(Bot bot)
    {
        _wereHouse.Add();
    }

    private void State()
    {
        if (IsBuilding && _wereHouse.ResourceCount == _resourceCreateBase)
        {
            CreateBase();
            _wereHouse.Remove(_resourceCreateBase);
        }
        else if(IsBuilding == false && _wereHouse.ResourceCount == _resourceCreateBots)
        {
            SpawnBots();
            _wereHouse.Remove(_resourceCreateBots);
        }
    }
}