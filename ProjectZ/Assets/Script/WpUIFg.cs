using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpUIFg : WpUI
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    protected override void UpdateImformation()
    {
        curAmmo = wpControlloer.curAmmo;
        maxAmmo = wpControlloer.maxAmmo;
        
        maxAmmoText.text = ((int)(curAmmo / maxAmmo * 100)).ToString() + "%";
    }
}
