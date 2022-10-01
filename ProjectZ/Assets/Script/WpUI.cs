using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WpUI : MonoBehaviour
{
    public WeaponController wpControlloer;
    public float curAmmo;
    public float maxAmmo;
    public Text curAmmoText;
    public Text maxAmmoText;

    // Start is called before the first frame update
    protected void Start()
    {
        
    }
    protected void OnEnable()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateImformation();
    }

    protected virtual void UpdateImformation()
    {
        curAmmo = wpControlloer.curAmmo;
        maxAmmo = wpControlloer.maxAmmo;
        curAmmoText.text = ((int)curAmmo).ToString();
        maxAmmoText.text = "/" + ((int)maxAmmo).ToString();
    }
}
