using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.Core.VfxSystem
{
    [Serializable]
    public class VfxWrapper<Ttype> where Ttype : Enum
    {
       public GameObject VfxPrefab;
       public Ttype Type;

    }
}
