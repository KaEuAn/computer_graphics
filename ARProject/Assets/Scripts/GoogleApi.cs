using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using Newtonsoft.Json;

public class GoogleApi : MonoBehaviour
{
    private List<File> files;
    private string json;
    public File keyfile = null;
    public Dictionary<string, string> textForWalls = null;
    public bool hasChanges = false;

    void Start()
    {
        GoogleDriveFiles.List().Send().OnDone += fileList => files = fileList.Files;
        //should change to Update if delete file during usage;
    }

    void Update()
    {
        if (!(files is null) && files.Count != 0 && keyfile is null)
        {
            keyfile = files[0];
            GoogleDriveFiles.Download(keyfile.Id).Send().OnDone += DownloadFile;
        }
        if (hasChanges && !(textForWalls is null))
        {
            json = JsonConvert.SerializeObject(textForWalls);
            Debug.Log("Uploading json " + json);
            keyfile.Content = Encoding.UTF8.GetBytes(json);
            var updateFile = new File() { Content = keyfile.Content };
            GoogleDriveFiles.Update(keyfile.Id, updateFile).Send();
            hasChanges = false;
        }
    }

    void DownloadFile(File file)
    {
        json = Encoding.UTF8.GetString(file.Content);
        Debug.Log("Downloading json " + json);
        textForWalls = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        keyfile = file;
    }
}