using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimapUI : Singleton<MinimapUI>
{
    public RectTransform startLine;
    public RectTransform endLine;

    public RectTransform anubisIcon;
    public RectTransform poseidonIcon;
    public RectTransform kaliIcon;
    public RectTransform thorIcon;
    
    private float startLineAnchoredPositionX;
    private float finishLineAnchoredPositionX;
    private float iconAnchoredPositionY;

    private List<int> godsPlayingIndex;
    private List<RectTransform> godsPlayingRectTransform;

    private void Awake()
    {
        godsPlayingIndex = new List<int>();
        godsPlayingRectTransform = new List<RectTransform>();

        if (!StoreGodInfo.Instance.anubisIA)
        {
            anubisIcon.gameObject.SetActive(false);
            godsPlayingIndex.Add(0);
            godsPlayingRectTransform.Add(anubisIcon);
        }

        if (!StoreGodInfo.Instance.poseidonIA)
        {
            poseidonIcon.gameObject.SetActive(false);
            godsPlayingIndex.Add(1);
            godsPlayingRectTransform.Add(poseidonIcon);
        }

        if (!StoreGodInfo.Instance.kaliIA)
        {
            kaliIcon.gameObject.SetActive(false);
            godsPlayingIndex.Add(2);
            godsPlayingRectTransform.Add(kaliIcon);
        }

        if (!StoreGodInfo.Instance.thorIA)
        {
            thorIcon.gameObject.SetActive(false);
            godsPlayingIndex.Add(3);
            godsPlayingRectTransform.Add(thorIcon);
        }
    }


    void Update()
    {
        for(int i = 0; i < godsPlayingIndex.Count; i++)
            UpdateIcons(godsPlayingRectTransform[i],godsPlayingIndex[i]);
    }

    private void UpdateIcons(RectTransform icon, float percentage)
    {
        float changeX = Mathf.Lerp(startLineAnchoredPositionX, finishLineAnchoredPositionX, percentage);
        icon.anchoredPosition = new Vector2(changeX, iconAnchoredPositionY);
    }

    public void Init()
    {
        iconAnchoredPositionY = startLine.anchoredPosition.y;
        startLineAnchoredPositionX = startLine.anchoredPosition.x;
        finishLineAnchoredPositionX = endLine.anchoredPosition.x;
    }
}
