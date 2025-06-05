using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Warehouse))]
[RequireComponent(typeof(Flag))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _storagePosition;

    private List<Bot> _freeBots = new();
    private List<Bot> _busyBots = new();

    private Storage _storage;
    private Warehouse _wereHouse;
    private BotsCreate _botsCreate;

    private int _resourceCreateBots = 3;
    private int _resourceBuildBase = 5;

    public bool IsBuild { get; private set; } = false;

    public Flag Flag { get; private set; }

    public int BotsCount { get; private set; }

    private void Awake()
    {
        _wereHouse = GetComponent<Warehouse>();
    }

    private void Start()
    {
        StartCoroutine(WalkGetResource());
    }

    public void init(Storage dataBase, BotsCreate botsCreate)
    {
        _storage = dataBase;
        _botsCreate = botsCreate;
    }

    private IEnumerator WalkGetResource()
    {
        while (enabled)
        {
            if (_freeBots.Count > 0)
            {
                Resource resource = _storage.SetTarget();

                if (resource != null)
                {
                    SetBotStatus(_freeBots[0], resource.transform);
                }
            }

            yield return null;
        }
    }

    public void StartGame(Bot bot)
    {
        bot.GetPositionStorage(_storagePosition);
        _freeBots.Add(bot);
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
        State();
    }

    private void State()
    {
        BotsCount = _freeBots.Count + _busyBots.Count;

        if (Flag != null)
        {
            BuildingBase();
        }
        else
        {
            CraftBot();
        }
    }

    public void CraftBot()
    {
        if (_wereHouse.ResourceCount == _resourceCreateBots)
        {
            Spawn();
            _wereHouse.Remove();
        }
    }

    public void BuildingBase()
    {
        if (_wereHouse.ResourceCount == _resourceBuildBase)
        {
            Bot bot = _freeBots[0];

            _freeBots.Remove(bot);
            _busyBots.Add(bot);
            bot.FinishCreated += FinishBuildingBase;
            bot.WalkTarget(Flag.transform);
            bot.Init(_storage, _botsCreate);
            _wereHouse.Remove();
        }
    }

    public void SetFlag(Flag flag)
    {
        Flag = flag;
        Flag.SetActive();
    }

    private void IsBuilding()
    {
        IsBuild = true;
    }

    private void Spawn()
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

    public void FinishBuildingBase(Flag flag, Bot bot)
    {
        Flag = null;
        IsBuilding();
        flag.SetInactive();
        bot.FinishCreated -= FinishBuildingBase;
        _busyBots.Remove(bot);
    }
}
