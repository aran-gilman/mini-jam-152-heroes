using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject segmentPrefab;

    [SerializeField]
    private bool hasLeftEnd;

    [SerializeField]
    private bool hasRightEnd;

    [Serializable]
    private class Segment
    {
        public Sprite Empty;
        public Sprite Full;
    }

    [SerializeField]
    private Segment leftEndSegment;

    [SerializeField]
    private Segment midSegment;

    [SerializeField]
    private Segment rightEndSegment;

    [SerializeField]
    private int maxValue;

    public int CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = value;
            for (int i = 0; i < maxValue; i++)
            {
                segments[i].sprite = GetSpriteForSegment(i);
            }
        }
    }

    private int currentValue;
    private readonly List<Image> segments = new List<Image>();

    private bool hasRetrievedChildren = false;

    public void SetMaxValue(int newMax, int newCurrentValue)
    {
        if (!hasRetrievedChildren)
        {
            RetrieveChildren();
        }

        maxValue = newMax;
        currentValue = newCurrentValue;

        RectTransform barRect = GetComponent<RectTransform>();
        RectTransform segmentRect = segmentPrefab.GetComponent<RectTransform>();
        barRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, segmentRect.sizeDelta.x * maxValue);

        // Add any missing segments
        for (int i = segments.Count; i < maxValue; i++)
        {
            GameObject newSegment = Instantiate(segmentPrefab, transform);
            segments.Add(newSegment.GetComponent<Image>());
        }

        // Activate visible segments and give them the correct sprites
        for (int i = 0; i < maxValue; i++)
        {
            segments[i].gameObject.SetActive(true);
            segments[i].sprite = GetSpriteForSegment(i);
        }

        // Deactivate segments beyond the max value
        for (int i = maxValue; i < segments.Count; i++)
        {
            segments[i].gameObject.SetActive(false);
        }
    }

    private Sprite GetSpriteForSegment(int i)
    {
        Sprite fullSprite = midSegment.Full;
        Sprite emptySprite = midSegment.Empty;

        if (hasLeftEnd)
        {
            if (i == 0)
            {
                fullSprite = leftEndSegment.Full;
                emptySprite = leftEndSegment.Empty;
            }
        }

        if (hasRightEnd)
        {
            if (i == maxValue - 1)
            {
                fullSprite = rightEndSegment.Full;
                emptySprite = rightEndSegment.Empty;
            }
        }
        return i < currentValue ? fullSprite : emptySprite;
    }

    private void RetrieveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                segments.Add(image);
            }
        }
        hasRetrievedChildren = true;
    }

    private void Awake()
    {
        SetMaxValue(maxValue, maxValue);
    }
}
