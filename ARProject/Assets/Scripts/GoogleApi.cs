using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using Newtonsoft.Json;

public class GoogleApi : MonoBehaviour
{


    private List<UnityGoogleDrive.Data.File> files;
    public UnityGoogleDrive.Data.File keyfile = null;
    public string json;
    public Dictionary<string, string> text_for_walls = null;
    public bool hasChanges = false;


    void Start()
    {
        GoogleDriveFiles.List().Send().OnDone += fileList => files = fileList.Files;
        Debug.Log("suka");
        //should change to Update if delete file during usage;
    }

    void Update()
    {
        Debug.Log(file.Count);
        if (!(files is null) && files.Count != 0 && keyfile is null)
        {
            keyfile = files[0];
            GoogleDriveFiles.Download(keyfile.Id).Send().OnDone += file => text_for_walls = JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.UTF8.GetString(file.Content));
            Debug.Log("got text_for_walls " + text_for_walls["wall3"]);
        }
        if (hasChanges)
        {
            var file = new UnityGoogleDrive.Data.File { Name = "keys.json", Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(text_for_walls)) };
            GoogleDriveFiles.Create(file).Send();
            hasChanges = false;
        }
    }
}