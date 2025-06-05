using TMPro;
using UnityEngine;

[RequireComponent(typeof(Warehouse))]
public class WarehouseView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private Warehouse _storage;

    private void Awake()
    {
        _storage = GetComponent<Warehouse>();
    }

    private void OnEnable()
    {
        _storage.ValueChanged += OnValueChange;
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= OnValueChange;
    }

    private void OnValueChange(int value)
    {
        _textMeshProUGUI.text = value.ToString();
    }
}
