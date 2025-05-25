using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BotAnimator))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _bag;

    private BotAnimator _botAnimator;
    private Transform _startPosition;
    private NavMeshAgent _agent;
    private Transform _walkTarget;

    private int _distance = 1;

    public event Action<Bot> Remove;

    public Resource _resource { get; private set; }

    private void Awake()
    {
        _botAnimator = GetComponent<BotAnimator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_walkTarget != null)
        {
            _agent.SetDestination(_walkTarget.position);

            if ((transform.position - _walkTarget.position).sqrMagnitude < _distance)
            {
                if (_walkTarget.TryGetComponent(out Resource resource))
                {
                    TakeResource(resource);
                }
                else
                {
                    NewTarget();
                }
            }
        }
    }

    private void TakeResource(Resource resource)
    {
        resource.IsTake();
        _resource = resource;
        resource.transform.SetParent(_bag, false);
        resource.transform.position = _bag.transform.position;
        WalkTarget(_startPosition);
    }

    private void NewTarget()
    {
        _walkTarget = null;
        Remove.Invoke(this);
        _botAnimator.Run(false);

        if (_resource != null)
        {
            _resource.transform.parent = null;
            _resource.Remove();
            _resource = null;
        }
    }

    public void WalkTarget(Transform target)
    {
        if (target.TryGetComponent(out Resource resource))
        {
            _walkTarget = resource.transform;
        }
        else
        {
            _walkTarget = target;
        }

        _botAnimator.Run(true);
    }

    public void InitStartPosition(Transform position)
    {
        _startPosition = position.transform;
    }
}
