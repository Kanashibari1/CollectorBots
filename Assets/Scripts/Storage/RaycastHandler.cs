using System;
using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public event Action<RaycastHit> Clicked;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                Clicked.Invoke(hit);
        }
    }
}
