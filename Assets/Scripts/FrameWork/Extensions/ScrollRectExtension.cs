﻿
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ScrollRect组件扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class ScrollRectExtension
{
    /// <summary>
    /// 滚动到指定位置
    /// </summary>
    /// <param name="scrollRect"></param>
    /// <param name="time"></param>
    /// <param name="nx"></param>
    /// <param name="ny"></param>
    [UnityEngine.Scripting.Preserve]
    public static void ScrollTo(this ScrollRect scrollRect, float time, float nx, float ny)
    {
        DOTween.Kill(scrollRect.gameObject);
        Tween tween = DOTween.To(() => scrollRect.normalizedPosition, x => scrollRect.normalizedPosition = x, new Vector2(nx, ny), time);
        tween.SetTarget(scrollRect.gameObject);
        tween.SetLink(scrollRect.gameObject);
        tween.SetAutoKill(true);
        tween.Play();
    }
}
