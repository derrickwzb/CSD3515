using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Utils
{
    public static void ChangeImage(VisualElement itemImg, string picPath)
    {
        Texture2D tmpTexture = Resources.Load<Texture2D>(picPath);
        StyleBackground pic = new StyleBackground(tmpTexture);
        itemImg.style.backgroundImage = pic;
        itemImg.style.unityBackgroundScaleMode = new StyleEnum<ScaleMode>(ScaleMode.ScaleToFit);
    }
}