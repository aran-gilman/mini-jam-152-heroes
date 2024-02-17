using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private Health target;

    private Text textDisplay;
    private string textFormat;

    private ProgressBar barDisplay;

    private void OnHealthChange()
    {
        if (textDisplay != null)
        {
            textDisplay.text = string.Format(
                textFormat, target.CurrentHealth, target.MaxHealth);
        }

        if (barDisplay != null)
        {
            barDisplay.CurrentValue = target.CurrentHealth;
        }
    }

    private void OnMaxHealthChange()
    {
        if (textDisplay != null)
        {
            textDisplay.text = string.Format(
                textFormat, target.CurrentHealth, target.MaxHealth);
        }

        if (barDisplay != null)
        {
            barDisplay.SetMaxValue(target.MaxHealth, target.CurrentHealth);
        }
    }

    private void Awake()
    {
        textDisplay = GetComponentInChildren<Text>();
        if (textDisplay != null)
        {
            textFormat = textDisplay.text;
        }

        barDisplay = GetComponentInChildren<ProgressBar>();
    }

    private void OnEnable()
    {
        target.OnHealthChange.AddListener(OnHealthChange);
        target.OnMaxHealthChange.AddListener(OnMaxHealthChange);
    }

    private void Start()
    {
        OnMaxHealthChange();
    }

    private void OnDisable()
    {
        target.OnHealthChange.RemoveListener(OnHealthChange);
        target.OnMaxHealthChange.RemoveListener(OnMaxHealthChange);
    }
}