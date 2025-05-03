using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.Core.VfxSystem
{
    public class BaseVfxController<Ttype> : MonoBehaviour where Ttype : Enum
    {
        public Action CallBack;
        public Ttype type;
        [SerializeField] 
        private float _lifeTime;

        protected virtual void Start()
        {

        }

        public virtual void Setup()
        {

        }

    }
}
