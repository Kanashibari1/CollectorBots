using UnityEngine;
using System;

[RequireComponent(typeof(RaycastHandler))]
public class SelectorLocation : MonoBehaviour
{
    [SerializeField] private Flag _prefabFlag;

    private Base _base;
    private Flag _flag;
    private RaycastHandler _raycastHandler;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    private void Start()
    {
        _flag = Instantiate(_prefabFlag, transform.position, Quaternion.identity);
        _flag.TurnOffInactive();
    }

    private void OnEnable()
    {
        _raycastHandler.Clicked += Select;
    }

    private void OnDisable()
    {
        _raycastHandler.Clicked -= Select;
    }

    private void Select(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Base @base))
        {
            _base = @base;
            _base.BoiledBase += TurnOffActive;
        }
        else if (hit.collider.TryGetComponent(out Ground _) && _base != null)
        {
            if (_base.BuildingCount == 0 && _base.BotsCount > 1)
            {
                _base.SetTarget(_flag.transform);
                _flag.TurnOnActive();
                _flag.transform.position = hit.point;
            }
        }
    }

    private void TurnOffActive()
    {
        _flag.TurnOffInactive();
        _base.BoiledBase -= TurnOffActive;
    }
}
