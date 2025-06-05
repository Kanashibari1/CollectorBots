using UnityEngine;
using System;

[RequireComponent(typeof(RaycastHandler))]
public class SelectorLocation : MonoBehaviour
{
    [SerializeField] private Flag _prefabFlag;

    private Flag _flag;
    private Base _base;
    private RaycastHandler _raycastHandler;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastHandler>();
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

            if (_flag == null)
            {
                _flag = Instantiate(_prefabFlag, @base.transform.position, Quaternion.identity);
            }
        }
        else if (hit.collider.TryGetComponent(out Ground _) && _base != null)
        {
            if (_base.IsBuild != true && _base.BotsCount > 1)
            {
                _flag.transform.position = hit.point;
                _base.SetFlag(_flag);
            }
        }
    }
}
