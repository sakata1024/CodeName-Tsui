using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {
    public Text showText;
	
    public class Profile
    {
        public Info info;

        [System.Serializable]
        public class Info
        {
            public string seed;
            public string version;
        }
    }

    public class SendData
    {
        public string id;
        public User user;

        [System.Serializable]
        public class User
        {
            public string name;
            public int age;
            public string email;
            public bool result;

            public User(string name, int age, string email, bool result)
            {
                this.name = name;
                this.age = age;
                this.email = email;
                this.result = result;
            }
        }
    }

    public void ButtonDown()
    {
        JSONConnection.JSONGet((Profile n) =>
        {
            if (n != null)
            {
                showText.text = n.info.seed;
            }
        });
    }

    public void PostDown()
    {
        SendData.User user = new SendData.User("taro", 19, "example@examp.com", false);
        SendData profile = new SendData();
        profile.id = "ghijkl";
        profile.user = user;
        JSONConnection.JSONPost(profile,() => {
            showText.text = profile.user.name;
        });
    }
}
