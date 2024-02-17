using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private Health target;

    private Text display;
    private string textFormat;

    private void OnHealthChange()
    {
        display.text = string.Format(textFormat, target.CurrentHealth, target.MaxHealth);
    }

    private void Awake()
    {
        display = GetComponentInChildren<Text>();
        textFormat = display.text;
    }

    private void OnEnable()
    {
        target.OnHit.AddListener(OnHealthChange);
    }

    private void Start()
    {
        OnHealthChange();
    }

    private void OnDisable()
    {
        target.OnHit.RemoveListener(OnHealthChange);
    }
}
