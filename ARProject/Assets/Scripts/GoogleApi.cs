using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public class GoogleApi : MonoBehaviour
{
    private List<File> files;
    public string filename = "";

    void Start()
    {
        GoogleDriveFiles.List().Send().OnDone += fileList => files = fileList.Files;
    }

    void Update()
    {
        if (!(files is null) && files.Count != 0 && filename.Length == 0)
        {
            filename = files[0].Name;
        }
    }
}