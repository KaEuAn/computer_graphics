using UnityEngine;
using System.Collections;

public class TextMeshAdditor : MonoBehaviour
{
    public Material mat;
    public Font font;

    void Start()
    {


        // create 3d text mesh
        GameObject Text = new GameObject();

        TextMesh textMesh = (TextMesh)GetComponent(typeof(TextMesh));
        Text.AddComponent(textMesh);
        textMesh.font = font;
        MeshRenderer meshRenderer = gameObject.AddComponent(MeshRenderer);
        meshRenderer.material = mat;

        textMesh.text = "Hello World!";

    }
}