using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public string text = null;
    public string newText = null;
    public bool updateText = false;
    public bool checkoutText = true;
    public EWall targetImageEnum = EWall.kWall6;
    public GoogleApi googleApi;
    
    void Update()
    {
        if (googleApi.textForWalls is null)
        {
            Debug.LogWarning("Google Api is not initialized yet!");
            return;
        }

        if (checkoutText && googleApi.fileStatus == GoogleApi.EDownloadStatus.kDownloaded)
        {
            checkoutText = false;
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
