using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WpUIController : MonoBehaviour
{
    public List<WpUI> wpUI = new List<WpUI>();
    public int equipWpIndex;

    public Text testText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWpUI(int wpUIIndex)
    {
        testText.text = "33333333";
        if (equipWpIndex >= 0)
        {
            wpUI[equipWpIndex].gameObject.SetActive(false);
        }
        //if (0 <= wpUIIndex && wpUIIndex > wpUI.Count)
        //{
            equipWpIndex = wpUIIndex;
            wpUI[equipWpIndex].gameObject.SetActive(true);
        //}
    }
}
