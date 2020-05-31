using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCreator : MonoBehaviour
{
    public TextMeshPro imageText;
    public InsertTextCanvasHandler insertTextHandler;
    public TextHandler parentHandler;
    public bool usingNewText = false;
    public GoogleApi googleApi;

    void Start()
    {
    }

    void Update()
    {
        if (parentHandler.text is null)
        {
            Debug.LogWarning("Parent TextHandler is not ready yet!");
            return;
        }

        if (insertTextHandler.updateText && parentHandler.targetImageEnum == insertTextHandler.currentWall)
        {
            imageText.text = insertTextHandler.currentText;
            parentHandler.newText = imageText.text;
            insertTextHandler.updateText = false;
            parentHandler.updateText = true;
            usingNewText = true;
        }
        if (!usingNewText)
        {
            imageText.text = parentHandler.text;
        }
    }
}
