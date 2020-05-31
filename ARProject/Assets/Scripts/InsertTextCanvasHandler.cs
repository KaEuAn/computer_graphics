using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InsertTextCanvasHandler : MonoBehaviour
{
    public Canvas canvas;
    public Button submitButton;
    public Button cancelButton;
    public TMP_InputField input;
    public string currentText;

    void Start()
    {
        canvas.enabled = false;
        submitButton.onClick.AddListener(OnSubmit);
        cancelButton.onClick.AddListener(OnCancel);
        currentText = input.text;
    }

    void Update()
    {   
    }

    private void OnSubmit()
    {
        currentText = input.text;
        // todo: post to google docs
        canvas.enabled = false;
    }

    private void OnCancel()
    {
        input.text = currentText;
        canvas.enabled = false;
    }
}
