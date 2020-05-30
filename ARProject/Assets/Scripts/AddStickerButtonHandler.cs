using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class AddStickerButtonHandler : MonoBehaviour
{
    private const float kSecPerFrame = 0.03F;
    private const int kSpritesCount = 10;
    private int currentSprite = 0;
    private float timeLeft = kSecPerFrame;
    public Sprite[] sprites = new Sprite[kSpritesCount];
    public Image image;
    public UnityEngine.UI.Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }
   
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            image.sprite = sprites[(++currentSprite) % kSpritesCount];
            timeLeft = kSecPerFrame;
        }
    }

    private void OnClick()
    {
        MessageBox.Show("Как поспал, братишка?", "Hello!", MessageBoxButtons.OK);
    }
}
