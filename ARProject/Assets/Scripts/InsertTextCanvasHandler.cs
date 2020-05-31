using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public enum EWall
{
    kWall3,
    kWall4,
    kWall6,
}

public static class Extensions
{
    public static string GetName(this EWall wall)
    {
        if (wall == EWall.kWall3)
        {
            return "wall3";
        }
        else if (wall == EWall.kWall4)
        {
            return "wall4";
        }
        else if (wall == EWall.kWall6)
        {
            return "wall6";
        }
        else
        {
            throw new KeyNotFoundException("Error parsing EWall enum");
        }
    }
}

public class InsertTextCanvasHandler : MonoBehaviour
{
    public Canvas canvas;
    public Button submitButton;
    public Button cancelButton;
    public TMP_InputField input;
    public bool updateText = false;
    public string currentText = null;
    public EWall currentWall = EWall.kWall6;

    void Start()
    {
        canvas.enabled = false;
        submitButton.onClick.AddListener(OnSubmit);
        cancelButton.onClick.AddListener(OnCancel);
    }

    void Update()
    {   
    }

    private void OnSubmit()
    {
        currentText = input.text;
        updateText = true;

        canvas.enabled = false;
        input.text = "";
    }

    private void OnCancel()
    {
        canvas.enabled = false;
        input.text = "";
    }
}
