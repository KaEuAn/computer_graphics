using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public string text = null;
    public string newText = null;
    public bool updateText = false;
    public EWall targetImageEnum = EWall.kWall6;
    public GoogleApi googleApi;

    void Update()
    {
        bool googleApiActive = !(googleApi.textForWalls is null);
        if (!googleApiActive)
        {
            Debug.LogWarning("Google Api is not initialized yet!");
            return;
        }

        if (text is null || text.Length == 0)
        {
            text = googleApi.textForWalls[targetImageEnum.GetName()];
        }

        if (updateText) {
            googleApi.textForWalls[targetImageEnum.GetName()] = newText;
            text = newText;
            updateText = false;
            googleApi.hasChanges = true;
        }
    }
}
