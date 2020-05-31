using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GetPicture : MonoBehaviour
{

    public string stringResult;

    IEnumerator MakePictureRequest(string picture)
    {
        if (picture is null)
        {
            Debug.Log("nul in Picture REquest");
            yield return 0;
        }
        string url = @"https://yandex.ru/images/search?text=" + picture;
        WWW request = new WWW(url);
        yield return request;

        if (request.error != null)
        {
            Debug.Log("request error: " + request.error);
        }
        else
        {
            stringResult = request.text;
            string[] stringsForFind = { "<div class=\"serp - item serp", "\"url\":" , "\"url\":", "\"url\":" };
            foreach(string curString in stringsForFind)
            {
                int ind = stringResult.IndexOf(curString);
                stringResult = stringResult.Substring(ind + 7);
            }
            stringResult = stringResult.Substring(0, stringResult.IndexOf('"'));
            Debug.Log("request success");
            Debug.Log("returned data" + request.text);
        }
    }


    void Start()
    {
        StartCoroutine(MakePictureRequest("i"));
    }

}
