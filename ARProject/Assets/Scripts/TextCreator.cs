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
