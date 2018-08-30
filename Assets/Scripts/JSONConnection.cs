using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using System.Linq;

using APIConnection;

public static class JSONConnection {

    public delegate void OnCompleteJsonGet(Dictionary<string,object> result);
    public delegate void OnCompleteJsonPost();

    public enum APIType { user_create,user_show,user_update,user_destroy, position_add, position_destroy};

    /// <summary>
    /// GETして取得したJSON文字列を指定した型に変換する関数
    /// </summary>
    /// <typeparam name="Type">取得したいクラス</typeparam>
    /// <param name="callback">取得クラスを使用するcallback関数</param>
    public static void JSONGet(APIType enumType,Dictionary<string,string> parameter,OnCompleteJsonGet callback)
    {
        string uri="";
        switch (enumType)
        {
            case APIType.user_create:
                uri = "users/create?";
                break;
            case APIType.user_show:
                uri = "users/show?";
                break;
            case APIType.user_update:
                uri = "users/update?";
                break;
            case APIType.user_destroy:
                uri = "users/destroy?";
                break;
            case APIType.position_add:
                uri = "positions/add?";
                break;
            case APIType.position_destroy:
                uri = "positions/destroy?";
                break;
        }

        uri += string.Join("&", parameter.Select(c => string.Format("{0}={1}", c.Key, c.Value)).ToArray());

        WebAPIManager.APIGet(uri, (string jsonStr) =>
        {
            if (jsonStr == null)
            {
                callback(null);
            }
            else
            {
                var jsonparse = Json.Deserialize(jsonStr) as Dictionary<string, object>;
                callback(jsonparse);
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
