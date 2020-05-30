using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCreator : MonoBehaviour
{

    public string text = null;

    public TextHandler parentHandler;

    public bool usingNewText = false;

    void Start()
    {
        parentHandler = this.transform.parent.GetComponent<TextHandler>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<TextMesh>().text = text;
        }
        if (!(parentHandler.text is null) & ! usingNewText)
        {
            text = parentHandler.text;
        }
    }
}
