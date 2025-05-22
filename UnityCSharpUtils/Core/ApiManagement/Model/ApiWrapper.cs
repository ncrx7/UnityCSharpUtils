using System;

namespace UnityUtils.APIManagement.Model
{
    [Serializable]
    public class ApiWrapper<TItem>
    {
        public TItem[] Items;
    }
}
