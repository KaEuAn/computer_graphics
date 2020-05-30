using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public class GoogleApi : MonoBehaviour
{
    public int len = -1;
    public string name = "wall3";
    public UnityGoogleDrive.Data.FileList list;
    public List<File> LFile;

    void Start()
    {
        GoogleDriveFiles.List().Send().OnDone += fileList => list = fileList;
    }

    void Update()
    {
        if (!list.Equals(null))
        {
            LFile = list.Files;
            len = LFile.Count;
        }
    }
}