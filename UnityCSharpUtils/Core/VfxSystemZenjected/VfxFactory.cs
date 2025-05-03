using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace UnityUtils.Core.VfxSystem
{
    public class VfxFactory<TType> : IFactory<TType, Vector3, BaseVfxController<TType>> where TType : Enum
    {
        [Inject] private readonly Dictionary<TType, VfxPool<TType>> _vfxPoolMap;



        public BaseVfxController<TType> Create(TType type, Vector3 targetPos)
        {
            var pool = _vfxPoolMap[type];
            var vfxObject = pool.Spawn();
            vfxObject.transform.position = targetPos;
            vfxObject.type = type;
            return vfxObject;
        }
    }
}
