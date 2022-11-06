using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    //public UI VRUISys;
    public Sprite imageNomal;
    public Sprite imageHiglight;
    public Image imageComponent;
    public bool isHover = false;

    public void On_HoverBtn()
    {
        if (isHover) return;
        if (imageComponent != null)
        {
            imageComponent.sprite = imageHiglight;
        }
        isHover = true;
    }
    public void Off_HoverBtn()
    {
        if (!isHover) return;
        if (imageComponent != null)
        {
            imageComponent.sprite = imageNomal;
        }
        isHover = false;
    }
}
