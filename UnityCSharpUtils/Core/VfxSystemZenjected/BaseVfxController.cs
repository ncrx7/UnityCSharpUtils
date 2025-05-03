using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.Core.VfxSystem
{
    public class BaseVfxController<Ttype> : MonoBehaviour where Ttype : Enum
    {
        [SerializeField] private bool _isLifeTimeEndless;
        
        private Action _lifeTimeOutCallBack;
        private Ttype Type;
        private float _lifeTime;

        private CancellationTokenSource _cts;

        public virtual void Setup(Ttype type, float lifeTime, Action lifeTimeOutCallBack, int refreshRate = 1000)
        {
            Type = type;
            _lifeTime = lifeTime;
            _lifeTimeOutCallBack = lifeTimeOutCallBack;

            if (_isLifeTimeEndless)
                return;

            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();

            SetTimer(_cts.Token, refreshRate).Forget();
        }

        private async UniTask SetTimer(CancellationToken token, int refreshRate = 1000)
        {
            try
            {
                while (_lifeTime > 0)
                {
                    await UniTask.Delay(refreshRate, cancellationToken: token);
                    _lifeTime -= refreshRate / 1000f;
                }

                _lifeTimeOutCallBack?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("The vfx timer has cancelled. Please be careful.");
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

    }
}
