using System;
using Eflatun.SceneReference;


namespace UnityUtils.SceneManagement.Models
{
    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }
}
