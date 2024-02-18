using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    private Health target;

    private Text textDisplay;
    private string textFormat;

    private ProgressBar barDisplay;

    public void LocateTarget()
    {
        if (target != null)
        {
            UnsubscribeFromTarget();
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in objs)
        {
            Health healthComponent = obj.GetComponentInChildren<Health>();
            if (healthComponent != null)
            {
                target = healthComponent;
                break;
            }
        }

        if (target != null)
        {
            SubscribeToTarget();
        }
    }

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
        LocateTarget();
        if (target == null)
        {
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        OnMaxHealthChange();
    }

    private void OnDisable()
    {
        UnsubscribeFromTarget();
    }

    private void SubscribeToTarget()
    {
        target.OnHealthChange.AddListener(OnHealthChange);
        target.OnMaxHealthChange.AddListener(OnMaxHealthChange);
    }

    private void UnsubscribeFromTarget()
    {
        target.OnHealthChange.RemoveListener(OnHealthChange);   
        target.OnMaxHealthChange.RemoveListener(OnMaxHealthChange);
    }
}
