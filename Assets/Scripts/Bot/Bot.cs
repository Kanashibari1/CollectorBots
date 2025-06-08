using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BotAnimator))]
[RequireComponent(typeof(SpawnerBase))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _bag;

    private SpawnerBase _spawn;
    private BotAnimator _botAnimator;
    private Transform _startPosition;
    private NavMeshAgent _agent;
    private Transform _target;
    private Storage _storage;
    private BotCreates _botsCreate;

    private int _distance = 1;

    public event Action<Bot> Returned;
    public event Action<Bot> FinishCreated;

    public Resource Resource { get; private set; }

    private void Awake()
    {
        _spawn = GetComponent<SpawnerBase>();
        _botAnimator = GetComponent<BotAnimator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);

            if ((transform.position - _target.position).sqrMagnitude < _distance)
            {
                if (_target.TryGetComponent(out Resource resource))
                {
                    TakeResource(resource);
                }
                else if(_target.TryGetComponent(out Flag flag))
                {
                    CreateBase(flag.transform.position);
                }
                else
                {
                    Reset();
                }
            }
        }
    }

    private void Reset()
    {
        _target = null;
        Returned.Invoke(this);
        _botAnimator.Run(false);

        if (Resource != null)
        {
            Resource.transform.parent = null;
            Resource.Remove();
            Resource = null;
        }
    }

    public void Init(Storage storage, BotCreates botsCreate)
    {
        _storage = storage;
        _botsCreate = botsCreate;
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        _botAnimator.Run(true);
    }

    public void SetPositionStorage(Transform position)
    {
        _startPosition = position.transform;
    }

    private void TakeResource(Resource resource)
    {
        Resource = resource;
        resource.transform.SetParent(_bag, false);
        resource.transform.position = _bag.transform.position;
        SetTarget(_startPosition);
    }

    private void CreateBase(Vector3 position)
    {
        Base newBase = _spawn.Spawn(position, _storage, _botsCreate);
        _target = null;
        newBase.AddBotNewBase(this);
        FinishCreated.Invoke(this);
    }
}
