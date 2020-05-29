using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCreator : MonoBehaviour
{

    public string text = "xxxVanya petuhxxx";

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<TextMesh>().text = text;
        }
    }
}
