using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public string text = null;
    public string newText = null;
    public string targetImageName = "wall3";
    public GoogleApi googleApi;

    void Start()
    {
        googleApi = GameObject.Find("ARCamera").GetComponent<GoogleApi>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (! (googleApi.text_for_walls is null) && !(text is null))
        {
            text = googleApi.text_for_walls[targetImageName];
        }

        if (!(newText is null) && !(googleApi.text_for_walls is null)) {
            googleApi.text_for_walls[targetImageName] = newText;
            text = newText;
            newText = null;
            googleApi.hasChanges = true;
        }
    }
}
