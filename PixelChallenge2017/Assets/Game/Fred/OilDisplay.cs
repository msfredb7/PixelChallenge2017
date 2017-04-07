using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilDisplay : Singleton<OilDisplay>
{
    public MouseEnterListener listener;
    public Image fill;
    private Vector2 fullSizeDelta;

    void Start()
    {
        fullSizeDelta = fill.rectTransform.sizeDelta;
    }
    public static void UpdateOil(float ratio)
    {
        instance.fill.rectTransform.sizeDelta = new Vector2(instance.fullSizeDelta.x, instance.fullSizeDelta.y * ratio);
    }
    public static bool IsMouseIn()
    {
        return instance.listener.isIn;
    }
}
