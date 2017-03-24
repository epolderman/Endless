using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Utility
{
    //Creates & initializes a new text element inside the given parent.
    public static Text NewText(string text, Transform parent)
    {
        RectTransform textRect = NewUIElement("Text", parent);
        Text t = textRect.gameObject.AddComponent<Text>();
        t.text = text;
        t.color = Color.black;
        t.alignment = TextAnchor.MiddleCenter;
        ScaleRect(textRect, 0, 0);
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        return t;
    }

    //Creates & initializes a button(with a text child) inside of the given parent.
    public static Button NewButton(string name, string text, Transform parent)
    {
        RectTransform btnRect = NewUIElement(name, parent);
        btnRect.gameObject.AddComponent<Image>();
        btnRect.gameObject.AddComponent<Button>();
        ScaleRect(btnRect, 160, 30);
        NewText(text, btnRect);

        return btnRect.GetComponent<Button>();
    }

    //Sets width and height with current anchors
    public static void ScaleRect(RectTransform r, float w, float h)
    {
        //Setting size along horizontal axis (width)
        r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);

        //Setting size along vertical axis (height)
        r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }


    //Creates & initializes an empty recttransform inside the given parent.
    public static RectTransform NewUIElement(string name, Transform parent)
    {
        RectTransform temp = new GameObject().AddComponent<RectTransform>();
        temp.name = name;
        temp.gameObject.layer = 5;
        temp.SetParent(parent);
        temp.localScale = new Vector3(1, 1, 1);
        temp.localPosition = new Vector3(0, 0, 0);
        return temp;
    }





}


