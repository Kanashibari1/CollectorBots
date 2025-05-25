using TMPro;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    [SerializeField] private Storage _scoreCounter;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        _scoreCounter.ValueChanged += OnValueChange;
    }

    private void OnDisable()
    {
        _scoreCounter.ValueChanged -= OnValueChange;
    }

    private void OnValueChange(int value)
    {
        _textMeshProUGUI.text = value.ToString();
    }
}
