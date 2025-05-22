using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem.Controllers.Converters
{
    public class VerticalConverter : ACoordinateConverter
    {
        public override Vector3 Forward => Vector3.forward;

        /// <summary>
        /// x and y is coordinate of grid (x , y is not unity world position, just a quee of the grid)
        /// TR: x ve y unity world space kordinatı değildir, grid kordinatıdır.
        /// </summary>
        public override Vector3 GridToWorld(int x, int y, float cellSize, Vector3 origin)
        {
            return new Vector3(x, y) * cellSize + origin; 
        }

        public override Vector3 GridToWorldCenter(int x, int y, float cellSize, Vector3 origin)
        {
            return new Vector3(x * cellSize + cellSize * 0.5f, y * cellSize + cellSize * 0.5f) + origin;
        }

        public override Vector2Int WorldToGrid(Vector3 worldPosition, float cellSize, Vector3 origin)
        {
            Vector3 gridPosition = (worldPosition - origin) / cellSize;
            var x = Mathf.FloorToInt(gridPosition.x);
            var y = Mathf.FloorToInt(gridPosition.y);
            return new Vector2Int(x, y);
        }
    }
}
