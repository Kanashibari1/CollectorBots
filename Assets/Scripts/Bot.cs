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

    public event Action<Bot> Returned;

    public Resource Resource { get; private set; }

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
                    Reset();
                }
            }
        }
    }

    private void Reset()
    {
        _walkTarget = null;
        Returned.Invoke(this);
        _botAnimator.Run(false);

        if (Resource != null)
        {
            Resource.transform.parent = null;
            Resource.Remove();
            Resource = null;
        }
    }

    public void WalkTarget(Transform target)
    {
        _walkTarget = target;

        _botAnimator.Run(true);
    }

    public void InitStartPosition(Transform position)
    {
        _startPosition = position.transform;
    }

    private void TakeResource(Resource resource)
    {
        Resource = resource;
        resource.transform.SetParent(_bag, false);
        resource.transform.position = _bag.transform.position;
        WalkTarget(_startPosition);
    }
}
