using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UserAPI {
    public delegate void OnComplete(bool flag);
    public delegate void OnCompleteGet(UserData userData);

    public class UserData
    {
        public string name;
        public int level;
        public int gold;
        public int exp;
        public Position_Log[] position_logs;

        [System.Serializable]
        public class Position_Log
        {
            public double latitude;
            public double longitude;
            public string time;
        }

        public UserData(Dictionary<string, object> UserDic)
        {
            this.name = (string)UserDic["name"];
            this.level = (int)((long)UserDic["level"]);
            this.gold = (int)((long)UserDic["gold"]);
            this.exp = (int)((long)UserDic["exp"]);
            List<Position_Log> positionlist = new List<Position_Log>();
            IList objs = (IList)UserDic["position_logs"];
            foreach(object obj in objs)
            {
                IList valueList = (IList)obj;
                Position_Log log = new Position_Log();
                log.latitude = (double)valueList[0];
                log.longitude = (double)valueList[1];
                log.time = (string)valueList[2];
                positionlist.Add(log);
            }
            this.position_logs = positionlist.ToArray();
        }
    }

    public static void CreateUser(string name, OnComplete callback = null)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("name", name);
        JSONConnection.JSONGet(JSONConnection.APIType.user_create, p, (Dictionary<string, object> result) =>
        {
            if(result==null || (string)result["status"] == "ERROR")
            {
                Debug.Log("ouch!");
                if (callback != null)
                {
                    callback(false);
                }
            }
            else if ((string)result["status"] == "OK")
            {
                PlayerPrefs.SetInt("id", (int)((long)result["id"]));
                PlayerPrefs.SetString("key", (string)result["access_key"]);

                if(callback != null)
                {
                    callback(true);
                }
            }
        });
    }

    public static void GetUser(OnCompleteGet callback)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("id", PlayerPrefs.GetInt("id").ToString());
        p.Add("access_key", PlayerPrefs.GetString("key"));
        JSONConnection.JSONGet(JSONConnection.APIType.user_show, p, (Dictionary<string, object> result) =>
        {
            if (result == null || (string)result["status"] == "ERROR")
            {
                Debug.Log("ouch!");
                if (callback != null)
                {
                    callback(null);
                }
            }
            else if ((string)result["status"] == "OK")
            {
                if (callback != null)
                {
                    UserData userData = new UserData(result);
                    callback(userData);
                }
            }
        });
    }

    public static void DeleteUser(OnComplete callback = null)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("id", PlayerPrefs.GetInt("id").ToString());
        p.Add("access_key", PlayerPrefs.GetString("key"));
        JSONConnection.JSONGet(JSONConnection.APIType.user_destroy, p, (Dictionary<string, object> result) =>
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

    public static void ChangeUserName(string name, OnComplete callback)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        p.Add("id", PlayerPrefs.GetInt("id").ToString());
        p.Add("access_key", PlayerPrefs.GetString("key"));
        p.Add("name", name);
        JSONConnection.JSONGet(JSONConnection.APIType.user_update, p, (Dictionary<string, object> result) =>
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
