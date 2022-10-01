using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainGUI;
    public GameObject MainUI;
    public GameObject MainGame;
    public GameObject OptionUI;
    public GameObject PowerUI;

    public int passivePoint = 10;
    public Text PointText;


    public GameObject[] ammoimage;
    public GameObject[] Speedimage;
    public GameObject[] damageimage;
    public GameObject[] HPimage;


    public int ammoCnt = 0;
    public int SpeedCnt = 0;
    public int damageCnt = 0;
    public int hpCnt = 0;

    public float AddDamage = 0;
    public float Addammo = 0;
    public float AddHP = 0;
    public float AddSpeed = 0;

    public GameObject DeadUIObj;

    public GameObject RightHand;
    public GameObject LeftHand;

    public GameObject LeftLaser;

 
    



    public enum KeyAction { UP, DOWN, RIGHT, LEFT, KEYCOUNT}
    void Start()
    {
        PointText.text ="잔여 포인트 : 10";
    }
    public void OnClick_InGame()
    {
        MainGUI.SetActive(false);
        MainGame.SetActive(true);
        RightHand.SetActive(true);
        LeftHand.SetActive(true);
        LeftLaser.SetActive(false);

    }
    public void OnClick_InOption()
    {
        MainUI.SetActive(false);
        OptionUI.SetActive(true);
    }
    public void OnClick_InPower()
    {
        MainUI.SetActive(false);
        PowerUI.SetActive(true);
    }

    public void OnClick_Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnClick_MainUI()
    {
        //재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_PowerExit()
    {
        MainUI.SetActive(true);
        PowerUI.SetActive(false);
    }
    public void OnClick_PowerPointReSet()
    {
        PointText.text = "잔여 포인트 : 10";
        passivePoint = 10;
        
        SpeedCnt = 0;
        damageCnt = 0;
        hpCnt = 0;
        int imgCnt = 5;
        for (int cnt = 0; imgCnt > cnt; cnt++)
        {
            ammoimage[cnt].SetActive(false);
        }
        for (int cnt = 0; imgCnt > cnt; cnt++)
        {
            Speedimage[cnt].SetActive(false);
        }
        for (int cnt = 0; imgCnt > cnt; cnt++)
        {
            damageimage[cnt].SetActive(false);
        }
        for (int cnt = 0; imgCnt > cnt; cnt++)
        {
            HPimage[cnt].SetActive(false);
        }
        ammoCnt = 0;
        AddDamage = 0;
        Addammo = 0;
        AddHP = 0;
        AddSpeed = 0;
    }

    public void OnClick_OptionExit()
    {
        MainUI.SetActive(true);
        OptionUI.SetActive(false);
    }

    public void OnClick_ammo()
    {

        if (ammoCnt >= 5 || (passivePoint <= 0))
        {
            return;
        }
        else
        {
            ammoCnt++;
            Addammo +=1;
        }
        for (int cnt = 0; ammoCnt > cnt; cnt++)
        {
            ammoimage[cnt].SetActive(true);
        }
        passivePoint--;
        PointText.text = "잔여 포인트 : "+passivePoint.ToString();

    }

    public void OnClick_speed()
    {

        if (SpeedCnt >= 5 || (passivePoint <= 0))
        {
            return;
        }
        else
        {
            SpeedCnt++;
            AddSpeed++;
        }
        for (int cnt = 0; SpeedCnt > cnt; cnt++)
        {
            Speedimage[cnt].SetActive(true);
        }
        passivePoint--;
        PointText.text = "잔여 포인트 : " + passivePoint.ToString();
    }

    public void OnClick_damage()
    {
        
        if (damageCnt >= 5 || (passivePoint <= 0))
        {
            return;
        }
        else
        {
            damageCnt++;
            AddDamage+=0.6f;
        }
        for (int cnt = 0; damageCnt > cnt; cnt++)
        {
            damageimage[cnt].SetActive(true);
        }
        passivePoint--;
        PointText.text = "잔여 포인트 : " + passivePoint.ToString();
    }

    public void OnClick_HP()
    {

        if (hpCnt >= 5 || (passivePoint <= 0))
        {
            return;
        }
        else
        {
            hpCnt++;
            AddHP += 20;
        }
        for (int cnt = 0; hpCnt > cnt; cnt++)
        {
            HPimage[cnt].SetActive(true);
        }
        passivePoint--;
        PointText.text = "잔여 포인트 : " + passivePoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
