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
            for (int i = 0; i < segments.Count; i++)
            {
                segments[i].sprite = GetSpriteForSegment(i);
            }
        }
    }

    private int currentValue;
    private readonly List<Image> segments = new List<Image>();

    public void SetMaxValue(int newMax, int newCurrentValue)
    {
        maxValue = newMax;

        RectTransform barRect = GetComponent<RectTransform>();
        RectTransform segmentRect = segmentPrefab.GetComponent<RectTransform>();
        barRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, segmentRect.sizeDelta.x * maxValue);

        for (int i = maxValue; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
            segments.RemoveAt(i);
        }

        for (int i = segments.Count; i < maxValue; i++)
        {
            GameObject newSegment = Instantiate(segmentPrefab, transform);
            segments.Add(newSegment.GetComponent<Image>());
        }
        CurrentValue = newCurrentValue;
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

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                segments.Add(image);
            }
        }
        SetMaxValue(maxValue, maxValue);
    }
}
