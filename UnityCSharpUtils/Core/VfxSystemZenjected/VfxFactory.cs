using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Zenject;

namespace UnityUtils.Core.VfxSystem
{
    public class VfxFactory<TType> : IFactory<TType, Vector3, float, int, BaseVfxController<TType>> where TType : Enum
    {
        [Inject] private readonly Dictionary<TType, VfxPool<TType>> _vfxPoolMap;

        public BaseVfxController<TType> Create(TType type, Vector3 targetPos, float lifeTime, int refreshRate = 1000)
        {
            var pool = _vfxPoolMap[type];

            var vfxObject = pool.Spawn();

            vfxObject.transform.position = targetPos;
           
            vfxObject.Setup(type, lifeTime, () => pool.Despawn(vfxObject), refreshRate);

            return vfxObject;
        }
    }
}
