using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using APIConnection;

public static class JSONConnection {

    public delegate void OnCompleteJson<T>(T t);
    public delegate void OnCompleteJsonPost();

    /// <summary>
    /// GETして取得したJSON文字列を指定した型に変換する関数
    /// </summary>
    /// <typeparam name="Type">取得したいクラス</typeparam>
    /// <param name="callback">取得クラスを使用するcallback関数</param>
    public static void JSONGet<Type>(OnCompleteJson<Type> callback) where Type : class
    {
        WebAPIManager.APIGet((string jsonStr) =>
        {
            if (jsonStr == null)
            {
                callback(null);
            }
            else
            {
                Type jsonParse = JsonUtility.FromJson<Type>(jsonStr);
                callback(jsonParse);
            }
        });
    }

    /// <summary>
    /// 指定したクラスオブジェクトをJSON文字列に変換してPOSTする関数
    /// </summary>
    /// <typeparam name="Type">POSTしたいクラスの型</typeparam>
    /// <param name="t">POSTしたいデータ</param>
    /// <param name="callback">callback関数</param>
    public static void JSONPost<Type>(Type t, OnCompleteJsonPost callback)
    {
        string jsonStr = JsonUtility.ToJson(t);
        WebAPIManager.APIPost(jsonStr, (string str) =>
        {
            if(str == null)
            {
                callback();
            }
            else
            {
                callback();
            }
        });
    }
}
