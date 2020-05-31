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
        googleApi = GameObject.Find("ARCamera").GetComponent<GoogleApi>();
    }

    void Update()
    {
        if (!(insertTextHandler.currentText is null) && imageText.text != insertTextHandler.currentText)
        {
            imageText.text = insertTextHandler.currentText;
            parentHandler.newText = imageText.text;
            usingNewText = true;
        }
        if (!(parentHandler.text is null) && !usingNewText)
        {
            imageText.text = parentHandler.text;
        }
    }
}
