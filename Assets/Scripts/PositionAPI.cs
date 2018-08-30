using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionAPI {
    public delegate void OnComplete(bool completeflag);

    public static void AddPosition(OnComplete callback = null)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("id", PlayerPrefs.GetInt("id").ToString());
        p.Add("access_key", PlayerPrefs.GetString("key"));
        p.Add("lat", 35.155021.ToString());
        p.Add("lng", 136.963806.ToString());
        p.Add("datetime", System.DateTime.Now.ToString());
        JSONConnection.JSONGet(JSONConnection.APIType.position_add, p, (Dictionary<string, object> result) =>
        {
            if (result == null || (string)result["status"] == "ERROR")
            {
                Debug.Log("ouch!");
                if (callback != null)
                {
                    callback(false);
                }
            }
            else if ((string)result["status"] == "OK")
            {
                if (callback != null)
                {
                    callback(true);
                }
            }
        });
    }

    public static void DeletePosition(OnComplete callback = null)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("id", PlayerPrefs.GetInt("id").ToString());
        p.Add("access_key", PlayerPrefs.GetString("key"));
        JSONConnection.JSONGet(JSONConnection.APIType.position_destroy, p, (Dictionary<string, object> result) =>
        {
            if (result == null || (string)result["status"] == "ERROR")
            {
                Debug.Log("ouch!");
                if (callback != null)
                {
                    callback(false);
                }
            }
            else if ((string)result["status"] == "OK")
            {
                if (callback != null)
                {
                    callback(true);
                }
            }
        });
    }
}
