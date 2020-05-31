using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCreator : MonoBehaviour
{
    public TextMeshPro imageText;
    public InsertTextCanvasHandler insertTextHandler;
    public TextHandler parentHandler;
    public GoogleApi googleApi;
    private bool waitForParentHanlerText = false;

    IEnumerator MakePictureRequest()
    {
        if (picture is null)
        {
            Debug.Log("nul in Picture REquest");
            yield return 0;
        }
        string url = @"https://yandex.ru/images/search?text=" + imageText;
        WWW request = new WWW(url);
        yield return request;

        if (request.error != null)
        {
            Debug.Log("request error: " + request.error);
        }
        else
        {
            stringResult = request.text;
            string[] stringsForFind = { "<div class=\"serp - item serp", "\"url\":", "\"url\":", "\"url\":" };
            foreach (string curString in stringsForFind)
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
    }

    void Update()
    {
        if (insertTextHandler.downloadText && !insertTextHandler.updateText)
        {
            Debug.Log("File download requested");
            googleApi.fileStatus = GoogleApi.EDownloadStatus.kDownloadRequested;
            insertTextHandler.downloadText = false;
            parentHandler.checkoutText = true;
            waitForParentHanlerText = true;
        }
        if (parentHandler.text is null)
        {
            Debug.LogWarning("Parent TextHandler is not ready yet!");
            return;
        }

        if (waitForParentHanlerText && !parentHandler.checkoutText)
        {
            UpdateImageText(parentHandler.text);
            waitForParentHanlerText = false;
        }
        if (insertTextHandler.updateText && parentHandler.targetImageEnum == insertTextHandler.currentWall)
        {
            UpdateImageText(insertTextHandler.currentText);
            parentHandler.newText = imageText.text;
            insertTextHandler.updateText = false;
            parentHandler.updateText = true;
        }
    }

    void UpdateImageText(string text)
    {
        imageText.text = text;
        // todo: add image update
    }
}
