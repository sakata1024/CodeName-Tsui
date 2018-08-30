using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {
    public Text showText;
    public InputField nameField;
    

    public void CreateDown()
    {
        UserAPI.CreateUser(nameField.text, (bool flag) => {
            if (flag)
            {
                Debug.Log("Create UserData!");
            }
            else
            {
                Debug.Log("Create failed...");
            }
        });
    }

    public void GetDown()
    {
        UserAPI.GetUser((UserAPI.UserData userData) => {
            if(userData != null)
            {
                showText.text = JsonUtility.ToJson(userData);
            }
        });
    }

    public void UpdateDown()
    {
        UserAPI.ChangeUserName(nameField.text, (bool flag) =>
        {
            if (flag)
            {
                Debug.Log("Update UserData!");
            }
            else
            {
                Debug.Log("Update failed...");
            }
        });
    }

    public void DestroyDown()
    {
        UserAPI.DeleteUser((bool flag) =>
        {
            if (flag)
            {
                Debug.Log("Delete UserData!");
            }
            else
            {
                Debug.Log("Delete failed...");
            }
        });
    }

    public void AddDown()
    {
        PositionAPI.AddPosition((bool flag)=> {
            if (flag)
            {
                Debug.Log("Add PositionData!");
            }
            else
            {
                Debug.Log("Add failed...");
            }
        });
    }

    public void DeleteDown()
    {
        PositionAPI.DeletePosition((bool flag)=> {
            if (flag)
            {
                Debug.Log("Delete PositionData!");
            }
            else
            {
                Debug.Log("Delete failed...");
            }
        });
    }
}
