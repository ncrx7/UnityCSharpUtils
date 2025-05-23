using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UnityUtils.Core.VfxSystem
{
    public class VfxPool<Ttype> : MonoMemoryPool<BaseVfxController<Ttype>> where Ttype : Enum
    {
        protected override void OnCreated(BaseVfxController<Ttype> item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(BaseVfxController<Ttype> item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(BaseVfxController<Ttype> item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
