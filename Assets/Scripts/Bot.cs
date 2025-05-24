using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _bag;

    private Transform _startPosition;
    private NavMeshAgent _agent;
    private int _distance = 1;
    private Transform _walkTarget;

    public event Action<Bot> Remove;

    public Resource _resource { get; private set; }

    private void Awake()
    {
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
    }

    public void InitStartPosition(Transform position)
    {
        _startPosition = position.transform;
    }

    private void TakeResource(Resource resource)
    {
        _resource = resource;
        resource.transform.SetParent(_bag, false);
        resource.transform.position = _bag.transform.position;
        WalkTarget(_startPosition);
    }

    private void NewTarget()
    {
        _walkTarget = null;
        Remove.Invoke(this);

        if (_resource != null)
        {
            _resource.transform.parent = null;
            _resource.Remove();
            _resource = null;
        }
    }
}
