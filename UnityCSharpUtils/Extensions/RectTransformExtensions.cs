using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public static class RectTransformExtensions
    {
        public static void RotateYConstantly(this RectTransform rectTransform, float degreesPerSecond)
        {
            float fullRotation = 360f;
            float duration = fullRotation / degreesPerSecond;

            rectTransform.DORotate(
                new Vector3(0f, fullRotation, 0f),
                duration,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(rectTransform.gameObject); 
        }
    }
}
