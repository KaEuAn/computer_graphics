using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using Newtonsoft.Json;

public class GoogleApi : MonoBehaviour
{
    public enum EDownloadStatus : ushort
    {
        kDownloadRequested,
        kFilesListRefreshed,
        kDownloaded,
        kInProgress
    }

    private List<File> files;
    private string json;
    public File keyfile = null;
    public Dictionary<string, string> textForWalls = null;
    public bool hasChanges = false;
    public EDownloadStatus fileStatus = EDownloadStatus.kDownloaded;
    public Dictionary<EWall, bool> textForWallReady = new Dictionary<EWall, bool>() { { EWall.kWall3, true }, { EWall.kWall4, true }, { EWall.kWall6, true }, };

    void Start()
    {
    }

    void Update()
    {
        if (fileStatus == EDownloadStatus.kDownloadRequested)
        {
            fileStatus = EDownloadStatus.kInProgress;
            GoogleDriveFiles.List().Send().OnDone += RefreshFileList;
        }
        if (fileStatus == EDownloadStatus.kFilesListRefreshed)
        {
            fileStatus = EDownloadStatus.kInProgress;
            keyfile = files[0];
            GoogleDriveFiles.Download(keyfile.Id).Send().OnDone += DownloadFile;
        }
        if (hasChanges && !(textForWalls is null) && fileStatus == EDownloadStatus.kDownloaded)
        {
            UpdateFile();
        }
    }

    void UpdateFile()
    {
        json = JsonConvert.SerializeObject(textForWalls);
        Debug.Log("Uploading json " + json);
        keyfile.Content = Encoding.UTF8.GetBytes(json);
        var updateFile = new File() { Content = keyfile.Content };
        GoogleDriveFiles.Update(keyfile.Id, updateFile).Send();
        hasChanges = false;
    }

    void RefreshFileList(FileList fileList)
    {
        files = fileList.Files;
        Debug.Log("File list refreshed, found " + files.Count.ToString() + " files");
        fileStatus = EDownloadStatus.kFilesListRefreshed;
    }

    void DownloadFile(File file)
    {
        fileStatus = EDownloadStatus.kDownloaded;
        json = Encoding.UTF8.GetString(file.Content);
        Debug.Log("Downloading json " + json);
        textForWalls = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        keyfile = file;
        textForWallReady = new Dictionary<EWall, bool>() { { EWall.kWall3, true }, { EWall.kWall4, true }, { EWall.kWall6, true }, };
    }
}