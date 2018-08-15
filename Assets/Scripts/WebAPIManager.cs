using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace APIConnection
{

    public class WebAPIManager : MonoBehaviour
    {

        public delegate void onComplete(string json);

        static MonoBehaviour monoBehaviour;

        private void Awake()
        {
            monoBehaviour = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// APIに対してGETする関数
        /// </summary>
        /// <param name="callback">json文字列を引数とするcallback関数</param>
        internal static void APIGet(onComplete callback)
        {
            monoBehaviour.StartCoroutine(APIGetCoroutine("https://randomuser.me/api/", callback));
        }

        /// <summary>
        /// APIに対してGETする関数
        /// </summary>
        /// <param name="uri">使用するURI</param>
        /// <param name="callback">json文字列を引数とするcallback関数</param>
        internal static void APIGet(string uri, onComplete callback)
        {
            monoBehaviour.StartCoroutine(APIGetCoroutine(uri, callback));
        }

        /// <summary>
        /// APIに対してPOSTする関数
        /// </summary>
        /// <param name="json">送信するJSON文字列</param>
        /// <param name="callback">文字列を引数とするcallback関数</param>
        internal static void APIPost(string json, onComplete callback)
        {
            monoBehaviour.StartCoroutine(APIPostCoroutine(json, "https://script.google.com/macros/s/AKfycbwqS4BYPkXVKnW7ZIdPpuduUymQkRMlD80pU24KM9b0diE0zlk/exec", callback));
        }

        /// <summary>
        /// APIに対してPOSTする関数
        /// </summary>
        /// <param name="json">送信するJSON文字列</param>
        /// <param name="uri">使用するURI</param>
        /// <param name="callback">json文字列を引数とするcallback関数</param>
        internal static void APIPost(string json, string uri, onComplete callback)
        {
            monoBehaviour.StartCoroutine(APIPostCoroutine(json, uri, callback));
        }


        /// <summary>
        /// GETメソッドを使用するコルーチン
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="callback">結果をstring型で受け取る関数</param>
        /// <returns></returns>
        static IEnumerator APIGetCoroutine(string uri, onComplete callback)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Accepted", "application/json");
                yield return www.SendWebRequest();

                Debug.Log("ResponseCode: " + www.responseCode);

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("GET_ERROR: " + www.error);
                    callback(null);
                }
                else
                {
                    callback(www.downloadHandler.text);
                }
            }
        }


        /// <summary>
        /// POSTメソッドを使用するコルーチン
        /// </summary>
        /// <param name="json">JSONデータ</param>
        /// <param name="uri">URI</param>
        /// <param name="callback">結果をstringで受け取る関数</param>
        /// <returns></returns>
        static IEnumerator APIPostCoroutine(string json, string uri, onComplete callback)
        {
            using (UnityWebRequest www = new UnityWebRequest(uri, "post"))
            {
                byte[] bodyraw = Encoding.UTF8.GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(bodyraw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                Debug.Log("ResponseCode: " + www.responseCode);

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("POST_ERROR: " + www.error);
                    callback(null);
                }
                else
                {
                    callback(www.downloadHandler.text);
                }
            }
        }
    }
}
