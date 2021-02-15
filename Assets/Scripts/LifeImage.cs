using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LifeImage : MonoBehaviour
{

    public Sprite LifeEmptySprite;
    public Sprite LifeFullSprite;

    public  Image _image;


    public void UpdateState(bool isFull)
    {
        _image.sprite = isFull ? LifeFullSprite : LifeEmptySprite;

    }

}

