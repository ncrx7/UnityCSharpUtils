using UnityEngine;
using DG.Tweening;
using System;

public static class GameObjectExtensions
{
    public static void DoElasticStretch(this GameObject gameObject, Vector3 stretchScale, float duration, Action callBack)
    {
        if (gameObject == null) return;

        Vector3 originalScale = gameObject.transform.localScale;

        gameObject.transform.localScale = stretchScale;

        gameObject.transform.DOScale(originalScale, duration)
            .SetEase(Ease.OutElastic).OnComplete(() => callBack?.Invoke()); 
           
    }
}
