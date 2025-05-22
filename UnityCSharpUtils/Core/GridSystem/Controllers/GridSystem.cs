using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem.Controllers.Converters;
using UnityUtils.GridSystem.Views;

namespace UnityUtils.GridSystem.Controllers
{
    public class GridSystem<T>
    {
        readonly int _width;
        readonly int _height;
        readonly float _cellSize;
        readonly Vector3 _origin;
        readonly T[,] _nodeGridArray;


        /// <summary>
        /// EN: To eliminate converter dependincies
        /// TR: Converter bagımlılıgını silmek için
        /// </summary>
        readonly ACoordinateConverter _coordinateConverter;

        public event Action<int, int, T> OnValueChangeEvent;

        /// <summary>
        /// EN: choose one In match3 launch horizontal or vertical to make grid. (factory methods)
        /// TR: Grid'in yataydamı dikeyde mi olacağını factory metotları ile karar veriyoruz
        /// </summary>

        public static GridSystem<T> VerticalGrid(int width, int height, float cellSize, Vector3 origin, bool debug = false)
        {
            return new GridSystem<T>(width, height, cellSize, origin, new VerticalConverter(), debug);
        }

        public static GridSystem<T> HorizontalGrid(int width, int height, float cellSize, Vector3 origin, bool debug = false)
        {
            return new GridSystem<T>(width, height, cellSize, origin, new HorizontalConverter(), debug);
        }

        public GridSystem(int width, int height, float cellSize, Vector3 origin, ACoordinateConverter coordinateConverter, bool debug)
        {
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;
            this._origin = origin;
            this._coordinateConverter = coordinateConverter ?? new VerticalConverter();

            _nodeGridArray = new T[width, height];

            if (debug)
            {
                DrawGridDebugLinesAndTexts();
            }
        }
        /// <summary>
        /// EN: methods for setting values from grid positions
        /// TR: Grid objelerini set etmek için setter metotları
        /// </summary>
        public void SetValue(Vector3 worldPosition, T value)
        {
            Vector2Int pos = _coordinateConverter.WorldToGrid(worldPosition, _cellSize, _origin);
            SetValue(pos.x, pos.y, value);
        }

        public void SetValue(int x, int y, T value)
        {
            if (IsValid(x, y))
            {
                _nodeGridArray[x, y] = value;
                OnValueChangeEvent?.Invoke(x, y, value);
            }
        }

        /// <summary>
        /// EN: methods for getting values from grid positions
        /// TR: Grid objelerini get etmek için getter metotları
        /// </summary>
        public T GetValue(Vector3 worldPosition)
        {
            Vector2Int pos = GetXY(worldPosition);
            return GetValue(pos.x, pos.y);
        }

        public T GetValue(int x, int y)
        {
            return IsValid(x, y) ? _nodeGridArray[x, y] : default(T);
        }

        /// <summary>
        /// EN: Grid Valid check
        /// TR: Verilen x ve y grid kordinatlarının var olup olmadığının kontrolü
        /// </summary>
        bool IsValid(int x, int y) => x >= 0 && y >= 0 && x < _width && y < _height;

        /// <summary>
        /// EN: Position methods according to converter type
        /// TR: kordinat dönüştürücümüzün türüne göre göre pozisyon fonksiyonları
        /// </summary>
        public Vector2Int GetXY(Vector3 worldPosition) => _coordinateConverter.WorldToGrid(worldPosition, _cellSize, _origin);

        public Vector3 GetWorldPositionCenter(int x, int y) => _coordinateConverter.GridToWorldCenter(x, y, _cellSize, _origin);

        Vector3 GetWorldPosition(int x, int y)
        {
            Vector3 coordinate = _coordinateConverter.GridToWorld(x, y, _cellSize, _origin);
            return coordinate;
        }

        /// <summary>
        /// EN: Debug Method
        /// TR: Debug metodu
        /// </summary>
        void DrawGridDebugLinesAndTexts()
        {
            const float duration = 100f;
            var parent = new GameObject("Debugging");

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    GridTextManager.CreateWorldText(parent, x + "," + y, GetWorldPositionCenter(x, y), _coordinateConverter.Forward);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, duration);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, duration);
                }
            }

            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, duration);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, duration);
        }
    }
}
