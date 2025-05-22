using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.SceneManagement.Controllers;
using UnityUtils.SceneManagement.Models;

namespace UnityUtils.SceneManagement.Views
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Image _loadingBar;
        [SerializeField] private float _fillSpeed = 0.5f;
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Camera _loadingCamera;
        [SerializeField] private SceneGroup[] _sceneGroups;

        private float _targetProgress;
        private bool _isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        async void Start()
        {
            await LoadSceneGroup(0, false);
        }

        private void Update()
        {
            if (!_isLoading)
                return;

            float currentFillAmount = _loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - _targetProgress);

            float dynamicFillSpeed = progressDifference * _fillSpeed;

            _loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, _targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async UniTask LoadSceneGroup(int index, bool reload)
        {
            _loadingBar.fillAmount = 0f;
            _targetProgress = 1;

            if (index < 0 || index >= _sceneGroups.Length)
            {
                Debug.LogError("Invalid scene group index!!");
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => _targetProgress = Mathf.Max(target, _targetProgress);

            EnableLoadingCanvas();
            await manager.LoadScenes(_sceneGroups[index], progress, reload);
            EnableLoadingCanvas(false);
        }

        private void EnableLoadingCanvas(bool enable = true)
        {
            _isLoading = enable;
            _loadingCanvas.gameObject.SetActive(enable);
            _loadingCamera.gameObject.SetActive(enable);
        }
    }

    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value)
        {
            Progressed?.Invoke(value / ratio);
        }
    }
}
