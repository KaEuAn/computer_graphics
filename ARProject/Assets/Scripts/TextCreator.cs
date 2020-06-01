using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCreator : MonoBehaviour
{
    public TextMeshPro stickerText;
    public SpriteRenderer stickerSpriteRenderer;
    public InsertTextCanvasHandler insertTextHandler;
    public TextHandler parentHandler;
    public GoogleApi googleApi;
    private bool waitForParentHanlerText = false;
    private const float targetImageSize = 1f;

    IEnumerator MakePictureRequest(string text)
    {
        if (text is null)
        {
            Debug.Log("nul in Picture REquest");
            yield return 0;
        }
        string url = @"https://yandex.ru/images/search?text=" + text;
        var request = new WWW(url);
        yield return request;

        if (request.error != null)
        {
            Debug.Log("request error: " + request.error);
            yield return 0;
        }

        var imageUrl = request.text;
        string[] stringsForFind = { "<div class=\"serp - item serp", "\"url\":", "\"url\":", "\"url\":" };
        foreach (string curString in stringsForFind)
        {
            int ind = imageUrl.IndexOf(curString);
            imageUrl = imageUrl.Substring(ind + 7);
        }
        imageUrl = imageUrl.Substring(0, imageUrl.IndexOf('"'));
        Debug.Log("Image url: " + imageUrl);

        var imageRequest = new WWW(imageUrl);
        yield return imageRequest;
        stickerSpriteRenderer.sprite = Sprite.Create(
            imageRequest.texture,
            new Rect(0, 0, imageRequest.texture.width, imageRequest.texture.height),
            new Vector2(0.5f, 0.5f),
            imageRequest.texture.height / targetImageSize
        );
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
            parentHandler.newText = stickerText.text;
            insertTextHandler.updateText = false;
            parentHandler.updateText = true;
        }
    }

    void UpdateImageText(string text)
    {
        stickerText.text = text;
        StartCoroutine(MakePictureRequest(text));
    }
}
