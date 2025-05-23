using System;
using UnityEngine;
using UnityEngine.Playables;

namespace UnityUtils.Data
{
    [Serializable]
    public class CinematicData<TType> where TType : Enum
    {
        public PlayableDirector Director;
        public TType Type;

        public void PlayCinematic()
        {
            Director.Play();
        }
    }
}