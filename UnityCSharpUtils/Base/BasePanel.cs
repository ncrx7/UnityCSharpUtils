using System;
using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;

namespace UnityUtils.BaseClasses
{
    public abstract class BasePanel<TType, TData> : MonoBehaviour where TType : Enum
    {
        public TType PanelType;
       
        protected void SetType(TType panelType)
        {
            PanelType = panelType;
        }

        public virtual void OnOpenPanel(TData data) { }

        public virtual void OnClosePanel(TData data) { }
    }
}
