using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUtils.StaticHelpers;

namespace UnityUtils.BaseClasses
{
    public class BaseGridBoardManager : MonoBehaviour
    {
        [Header("Board Settings")]
        [Range(0.1f, 1f)]
        [SerializeField] private float _xScreenUsageRate, _yScreenUsageRate;
        [SerializeField] private Vector3 _manuelOffset = Vector3.zero;
        [SerializeField] private Vector3 _offsetFromCenter = Vector3.zero;
        [SerializeField] private bool _automaticCellSizeScalerByScreen = false;
        [SerializeField] protected bool _debug;

        protected int _width;
        protected int _height;

        public float AutomaticBoardCellSize { get; private set; }
        private Vector3 AutomaticOffset = Vector3.zero;

        protected Vector3 _lastOffsetValue;

        public static Action OnSizeCalculatingStarted;
        public static Action OnSizeCalculatingEnd;


        protected async virtual void Start()
        {
            if (_automaticCellSizeScalerByScreen)
            {
                await CalculateDimensions();
                _lastOffsetValue = AutomaticOffset;
            }

            else
            {
                _lastOffsetValue = _manuelOffset;
            }
        }

        private async UniTask CalculateDimensions()
        {
            OnSizeCalculatingStarted?.Invoke();
            //_gameManager.IsGamePaused = true;

            float screenHeight = 2f * Camera.main.orthographicSize;
            float screenWidth = screenHeight * Camera.main.aspect;

            float maxGridWidth = screenWidth * _xScreenUsageRate;
            float maxGridHeight = screenHeight * _yScreenUsageRate;

            float availableCellWidth = (maxGridWidth) / _width;
            float availableCellHeight = (maxGridHeight) / _height;

            float cellSize = Mathf.Min(availableCellWidth, availableCellHeight);

            AutomaticBoardCellSize = cellSize;

            await UniTask.Delay(100);

            AutomaticOffset.x = -(_width / 2f * AutomaticBoardCellSize);
            AutomaticOffset.y = -(_height / 2f * AutomaticBoardCellSize);

            AutomaticOffset += _offsetFromCenter;

            OnSizeCalculatingEnd?.Invoke();
        }

        protected async UniTask InitGridEntity(Action gridItemCreator, int offset = 0)
        {
            for (int x = 0; x < _width + offset; x++)
            {
                for (int y = 0; y < _height + offset; y++)
                {
                    gridItemCreator?.Invoke();
                }
            }

            await UniTask.DelayFrame(1);
        }

        public void AutoScaleGridEntityByFit(SpriteRenderer entitySR)
        {
            var scaleFactor = SpriteAutoScaler.CalculateSpriteScaleFactor(entitySR, AutomaticBoardCellSize, 0.65f, 0.65f);
            entitySR.transform.localScale = Vector2.one * scaleFactor;
        }

        public void AutoScaleGridEntityByRate(SpriteRenderer entitySR, float cellXSizeRate = 1, float cellYSizeRate = 1)
        {
            var scaleFactor = SpriteAutoScaler.CalculateSpriteScaleFactor(entitySR, AutomaticBoardCellSize, cellXSizeRate, cellYSizeRate);
            entitySR.transform.localScale = Vector2.one * scaleFactor;
        }

        public int GetWidth => _width;
        public int GetHeight => _height;
    }
}
