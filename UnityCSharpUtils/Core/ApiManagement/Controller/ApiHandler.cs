using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityUtils.APIManagement.Model;

namespace UnityUtils.APIManagement.Controller
{
    public static class ApiHandler
    {
        /// <summary>
        /// I made this Util API Handler as a generic because
        /// I wanted it to be dynamic structere that can be used on All API which we can use in the Game.
        /// This method handle both List response data or single data.
        /// If returned response data is a collection, we can call a custom callback to parse this collection. 
        /// If returnes response data is a single data, it will match to data directly.
        /// </summary>
        public static async UniTask<ApiResult<T>> GetApiResponse<T>(string url, Func<string, T> customParser = null)
        {
            ApiResult<T> result = new ApiResult<T>();

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.downloadHandler = new DownloadHandlerBuffer();

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        string rawJson = request.downloadHandler.text;

                        if (customParser != null)
                        {
                            result.ResponseData = customParser.Invoke(rawJson);
                        }
                        else
                        {
                            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                            {
                                Type itemType = typeof(T).GetGenericArguments()[0];
                                string wrappedJson = "{ \"Users\": " + rawJson + " }";
                                var wrapperType = typeof(ApiWrapper<>).MakeGenericType(itemType);
                                object wrapperObj = JsonUtility.FromJson(wrappedJson, wrapperType);
                                result.ResponseData = (T)wrapperType.GetField("Items").GetValue(wrapperObj);
                            }
                            else
                            {
                                result.ResponseData = JsonUtility.FromJson<T>(rawJson);
                            }
                        }

                        result.IsError = false;
                        result.ResponseMessage = "Success";
                    }
                    catch (Exception ex)
                    {
                        result.IsError = true;
                        result.ResponseErrorMessage = "JSON Parse Error: " + ex.Message;
                    }
                }
                else
                {
                    result.IsError = true;
                    result.ResponseErrorMessage = $"Request Error: {request.responseCode} - {request.error}";
                }
            }

            return result;
        }
    }
}





