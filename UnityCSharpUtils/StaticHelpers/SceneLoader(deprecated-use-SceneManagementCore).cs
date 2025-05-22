using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityUtils.StaticHelpers
{
    public static class SceneLoader
    {
        public static async UniTask LoadSceneAsync(int sceneIndex)
        {
            GameEventHandler.OnSceneLoadStart?.Invoke();

            var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

            while(!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }

            GameEventHandler.OnSceneLoadFinished?.Invoke();
        }
    }
}
