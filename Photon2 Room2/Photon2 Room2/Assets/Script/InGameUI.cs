using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject WeaponGun;
    public GameObject WeaponFire;
    public GameObject PlayObj;
    public Text GunText;
    public Text FireGunText;
    public Text HPText;

    void Start()
    {
        
    }

    private void Awake()
    {
        if(WeaponGun != null)
        {
            Weapon Weapon = WeaponGun.GetComponent<Weapon>();
            GunText.text = Weapon.curAmmo.ToString() + "/" + Weapon.maxAmmo.ToString();
        }
        
        //curAmmo += Weapon.Addammo;
        if(WeaponFire != null)
        {
            Weapon WeaponFireGun = WeaponFire.GetComponent<Weapon>();
            int Fireammo = (int)WeaponFireGun.curAmmo;
            FireGunText.text = Fireammo.ToString() + " / " + WeaponFireGun.maxAmmo.ToString();
        }

        if (PlayObj != null)
        {
            Player play = PlayObj.GetComponent<Player>();
            HPText.text = play.heart.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponGun != null)
        {
            Weapon WeaponSub = WeaponGun.GetComponent<Weapon>();
            int ammo = (int)WeaponSub.curAmmo;
            GunText.text = ammo.ToString() + " / " + WeaponSub.maxAmmo.ToString();
        }

        if (WeaponFire != null)
        {
            Weapon WeaponFireGun = WeaponFire.GetComponent<Weapon>();
            int Fireammo = (int)WeaponFireGun.curAmmo;
            FireGunText.text = Fireammo.ToString() + " / " + WeaponFireGun.maxAmmo.ToString();
        }

        if (PlayObj != null)
        {
            Player play = PlayObj.GetComponent<Player>();
            HPText.text = play.heart.ToString();
        }

    }
}
