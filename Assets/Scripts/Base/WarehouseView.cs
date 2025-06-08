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
        _storage.ValueChanged += ValueChange;
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= ValueChange;
    }

    private void ValueChange(int value)
    {
        _textMeshProUGUI.text = value.ToString();
    }
}
