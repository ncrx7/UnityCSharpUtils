using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using Eflatun.SceneReference;
using System.Linq;

namespace UnityUtils.SceneManagement.Models
{
    public enum SceneType { ActiveScene, MainMenu, UserInterface, HUD, Cinematic, Environment, Tooling }

    [Serializable]
    public class SceneGroup
    {
        public string SceneGroupName = "New Scene Group";
        public List<SceneData> Scenes;
        public string FindSceneNameByType(SceneType type)
        {
            return Scenes.FirstOrDefault(scene => scene.SceneType == type)?.Reference.Name;
        }
    }
}
