using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.StaticHelpers
{
    public static class SpriteAutoScaler
    {
        public static Vector2 CalculateSpriteScaleFactor(SpriteRenderer spriteToScale, float targetSize, float targetXSizeRate = 1, float targetYSizeRate = 1)
        {
            float spriteUnitSizeX = spriteToScale.sprite.bounds.size.x;
            float spriteUnitSizeY = spriteToScale.sprite.bounds.size.y;

            float edgeScaleFactorX = (targetSize * targetXSizeRate) / spriteUnitSizeX;
            float edgeScaleFactorY = (targetSize * targetYSizeRate) / spriteUnitSizeY;

            return new Vector2(edgeScaleFactorX, edgeScaleFactorY);
        }
    }
}
