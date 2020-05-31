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

    void Start()
    {
    }

    void Update()
    {
        if (imageText.text != insertTextHandler.currentText)
        {
            imageText.text = insertTextHandler.currentText;
        }
        if (!(parentHandler.text is null) && !usingNewText)
        {
            imageText.text = parentHandler.text;
        }
    }
}
