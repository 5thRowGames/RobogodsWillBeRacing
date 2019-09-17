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


    void Update()
    {
        UpdateIcons(anubisIcon,MinimapControl.Instance.currentPercentageList[0]);
        UpdateIcons(poseidonIcon,MinimapControl.Instance.currentPercentageList[1]);
        UpdateIcons(kaliIcon,MinimapControl.Instance.currentPercentageList[2]);
        UpdateIcons(thorIcon,MinimapControl.Instance.currentPercentageList[3]);
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
