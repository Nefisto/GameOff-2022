using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static partial class Extensions
{
    public static void SetAlpha (this Image image, float alpha)
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }

    public static void SetAlpha (this TMP_Text text, float alpha)
    {
        var tempColor = text.color;
        tempColor.a = alpha;
        text.color = tempColor;
    }
}