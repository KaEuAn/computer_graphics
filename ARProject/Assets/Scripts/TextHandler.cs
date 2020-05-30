using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public string text = null;
    public string name = "wall3";


    // Update is called once per frame
    void Update()
    {
        GoogleApi googleApi = GameObject.Find("ARCamera").GetComponent<GoogleApi>();
        if (! (googleApi.text_for_walls is null))
        {
            text = googleApi.text_for_walls[name];
        }
    }
}
