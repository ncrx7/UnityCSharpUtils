using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.Core.VfxSystem
{
    [Serializable]
    public class VfxWrapper<Ttype> where Ttype : Enum
    {
       public GameObject VfxPrefab;
       public Ttype Type;

    }
}
