using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.GridSystem.Controllers;

namespace UnityUtils.GridSystem.Models
{
    public enum Direction { Left, Right, Up, Down }

    public class GridObject<T>
    {
        /// <summary>
        /// EN: We made this class generic so that it would be more dynamic and convenient for us to put another object instead of a node or another type of node.
        /// TR: Bu sınıfı node'larımın daha dinamik olması için generic yaptım.
        /// </summary>
        private GridSystem<GridObject<T>> _grid;
        private int _x;
        private int _y;
        private T _manager;

        private Dictionary<Direction, GridObject<T>> _neighborGridObjects = new();

        public GridObject(GridSystem<GridObject<T>> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
        }

        public void SetValue(T manager)
        {
            _manager = manager;
        }

        public T GetValue()
        {
            return _manager;
        }

        public void InitNeighbourGridObjects()
        {
            _neighborGridObjects[Direction.Right] = _grid.GetValue(_x + 1, _y);
            _neighborGridObjects[Direction.Left] = _grid.GetValue(_x - 1, _y);
            _neighborGridObjects[Direction.Up] = _grid.GetValue(_x, _y + 1);
            _neighborGridObjects[Direction.Down] = _grid.GetValue(_x, _y - 1);
        }

        public GridObject<T> GetNeighbourGridObject(Direction neighbourDirection)
        {
            return _neighborGridObjects[neighbourDirection];
        }

        public Dictionary<Direction, GridObject<T>> GetAllNeighbourGridObjects => _neighborGridObjects;
        public int GetX => _x;
        public int GetY => _y;
        public GridSystem<GridObject<T>> GetGridSystem => _grid;
    }
}
