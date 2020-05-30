using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public Button button;
    public Canvas insertTextCanvas;

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
        insertTextCanvas.enabled = true;
    }
}
