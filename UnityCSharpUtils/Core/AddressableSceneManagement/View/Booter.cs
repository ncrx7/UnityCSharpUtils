using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UnityUtils.SceneManagement.Views
{
    public class Booter : MonoBehaviour
    {
        public static Booter Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogError("Singleton booter broken!!");
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void Init()
        {
            Debug.Log("Booting game..");
            await SceneManager.LoadSceneAsync("BootScene", LoadSceneMode.Single);
        }
    }
}
