using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.APIManagement.Model
{
    [Serializable]
    public class ApiResult<T>
    {
        public T ResponseData;
        public bool IsError;
        public string ResponseMessage;
        public string ResponseErrorMessage;


        public bool CheckAnyResponseError()
        {
            return IsError;
        }
    }
}
