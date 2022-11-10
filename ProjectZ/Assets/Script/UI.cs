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
    public GameObject PassiveUI;
    public GameObject LevelUpUI;
    public GameObject PlayerUI;
    public GameObject FinishUI;
    public GameObject BossHpUI;
    public GameObject Enemy;

    public int passivePoint = 10;
    public int bufPassivePoint;
    public Text PointText;

    protected enum PsvType { speed, damage, hp, ammo }

    public List<PassiveCheckList> psvCheckLists;
    public List<PassiveCheckList> LevelpsvCheckLists;
    public WeaponController[] wpControllerList;
    public GameObject[] LvValImageList;


    public int speedCnt = 0;
    public int damageCnt = 0;
    public int hpCnt = 0;
    public int ammoCnt = 0;
    public int[] bufCntArr;

    public float AddSpeed = 0;
    public float AddDamage = 0;
    public float AddHP = 0;
    public float AddAmmo = 0;
    public float[] bufAddValArr;

    protected const float oneSpeedVal = 1;
    protected const float oneDamageVal = 0.6f;
    protected const float oneHPVal = 20;
    protected const float oneAmmoVal = 1;
    public float[] oneValArr = { 1, 0.6f, 20, 1 };


    public GameObject DeadUIObj;

    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject LeftLaser;

    public Image expGageImage;
    public Image bossHpGageImage;
    public Transform PlayerUIPos;
    public Transform PlayerPos;
    
 
    



    public enum KeyAction { UP, DOWN, RIGHT, LEFT, KEYCOUNT}
    void Start()
    {
        //PointText.text ="잔여 포인트 : 10";
        bufCntArr = new int[psvCheckLists.Count];
        bufAddValArr = new float[psvCheckLists.Count];
        bufPassivePoint = passivePoint;
    }
    public void OnClick_InGame()
    {
        MainUI.SetActive(false);
        MainGame.SetActive(true);
        RightHand.SetActive(true);
        LeftHand.SetActive(true);
        LeftLaser.SetActive(false);
        PlayerUI.SetActive(true);
    }
    public void OnClick_InOption()
    {
        MainUI.SetActive(false);
        OptionUI.SetActive(true);
    }
    public void OnClick_InPower()
    {
        MainUI.SetActive(false);
        PassiveUI.SetActive(true);
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
        passivePoint = bufPassivePoint;

        On_UpdatePsv();

        speedCnt = bufCntArr[(int)PsvType.speed];   //player
        damageCnt = bufCntArr[(int)PsvType.damage]; //bullet
        hpCnt = bufCntArr[(int)PsvType.hp];         //player
        ammoCnt = bufCntArr[(int)PsvType.ammo];     //wp

        AddSpeed = bufAddValArr[(int)PsvType.speed];
        AddDamage = bufAddValArr[(int)PsvType.damage];
        AddHP = bufAddValArr[(int)PsvType.hp];
        AddAmmo = bufAddValArr[(int)PsvType.ammo];

        

        MainUI.SetActive(true);
        PassiveUI.SetActive(false);
    }
    public void OnClick_LevelPowerExit()
    {
        passivePoint = bufPassivePoint;
        speedCnt = bufCntArr[(int)PsvType.speed];
        damageCnt = bufCntArr[(int)PsvType.damage];
        hpCnt = bufCntArr[(int)PsvType.hp];
        ammoCnt = bufCntArr[(int)PsvType.ammo];

        AddSpeed = bufAddValArr[(int)PsvType.speed];
        AddDamage = bufAddValArr[(int)PsvType.damage];
        AddHP = bufAddValArr[(int)PsvType.hp];
        AddAmmo = bufAddValArr[(int)PsvType.ammo];
        On_UpdatePsv();

        LevelUpUI.SetActive(false);
        LeftLaser.SetActive(false);
    }
    public void OnClick_PowerPointReSet()
    {
        passivePoint = 10;
        bufPassivePoint = passivePoint;
        PointText.text = "잔여 포인트 : " + bufPassivePoint.ToString();

        speedCnt = 0;
        damageCnt = 0;
        hpCnt = 0;
        ammoCnt = 0;
        AddDamage = 0;
        AddAmmo = 0;
        AddHP = 0;
        AddSpeed = 0;

        for(int i = 0; i < psvCheckLists.Count; i++)
        {
            bufCntArr[i] = 0;
            bufAddValArr[i] = 0f;
        }

        foreach(PassiveCheckList psvCheckList in psvCheckLists)
        {
            foreach(Image img in psvCheckList.checkIconList)
            {
                img.sprite = psvCheckList.imageNomal;
            }
        }

    }
    public void OnClick_PowerPointLevelReSet()
    {
        bufPassivePoint = passivePoint;
        Text levelPointText = GameObject.Find("LevelPointText").GetComponent<Text>();
        levelPointText.text = "잔여포인트 : " + bufPassivePoint.ToString();

        bufCntArr[(int)PsvType.speed] = speedCnt;
        bufCntArr[(int)PsvType.damage] = damageCnt;
        bufCntArr[(int)PsvType.hp] = hpCnt;
        bufCntArr[(int)PsvType.ammo] = ammoCnt;

        bufAddValArr[(int)PsvType.speed] = AddSpeed;
        bufAddValArr[(int)PsvType.damage] = AddDamage;
        bufAddValArr[(int)PsvType.hp] = AddHP;
        bufAddValArr[(int)PsvType.ammo] = AddAmmo;

        //1. checkList 이미지를 저장했던 패시브 능력치만큼 반영
        for (int i = 0; i < LevelpsvCheckLists.Count; i++)
        {
            for (int j = 0; j < LevelpsvCheckLists[i].checkIconList.Count; j++)
            {
                LevelpsvCheckLists[i].checkIconList[j].sprite = bufCntArr[i] > j ? LevelpsvCheckLists[i].imageHighlight : LevelpsvCheckLists[i].imageNomal;
            }
        }
    }
    public void OnClick_OptionExit()
    {
        MainUI.SetActive(true);
        OptionUI.SetActive(false);
    }

    public void RisePsv(int psvType, List<PassiveCheckList> pscChkList, Text text)
    {
        if (bufCntArr[psvType] >= 5 || (bufPassivePoint <= 0))
        {
            return;
        }
        else
        {
            bufCntArr[psvType]++;
            bufAddValArr[psvType] += oneValArr[psvType];
        }
        for (int cnt = 0; cnt < bufCntArr[psvType]; cnt++)
        {
            pscChkList[psvType].checkIconList[cnt].sprite = pscChkList[psvType].imageHighlight;
        }
        bufPassivePoint--;
        text.text = "잔여포인트 : " + bufPassivePoint.ToString();
    }
    public void OnClick_speed()
    {
        RisePsv((int)PsvType.speed, psvCheckLists, PointText);
    }
    public void OnClick_damage()
    {
        RisePsv((int)PsvType.damage, psvCheckLists, PointText);
    }
    public void OnClick_HP()
    {
        RisePsv((int)PsvType.hp, psvCheckLists, PointText);
    }
    public void OnClick_ammo()
    {
        RisePsv((int)PsvType.ammo, psvCheckLists, PointText);
    }
    public void OnClick_LevelSpeed()
    {
        RisePsv((int)PsvType.speed, LevelpsvCheckLists, GameObject.Find("LevelPointText").GetComponent<Text>());
    }
    public void OnClick_LevelDamage()
    {
        RisePsv((int)PsvType.damage, LevelpsvCheckLists, GameObject.Find("LevelPointText").GetComponent<Text>());
    }
    public void OnClick_LevelHP()
    {
        RisePsv((int)PsvType.hp, LevelpsvCheckLists, GameObject.Find("LevelPointText").GetComponent<Text>());
    }
    public void OnClick_LevelAmmo()
    {
        RisePsv((int)PsvType.ammo, LevelpsvCheckLists, GameObject.Find("LevelPointText").GetComponent<Text>());
    }

    public void On_LevelUp()
    {
        passivePoint++;
        LevelUpUI.SetActive(true);
        LeftLaser.SetActive(true);
        OnClick_PowerPointLevelReSet();
    }
    public void On_UpdatePsv()
    {
        Player player = GameObject.Find("OVRPlayerController").GetComponent<Player>();
        player.UpdateHp(bufAddValArr[(int)PsvType.hp] - AddHP);
        player.UpdateSpeed(bufAddValArr[(int)PsvType.speed] - AddSpeed);
        foreach(WeaponController wpController in wpControllerList)
        {
            wpController.UpdateAmmo(bufAddValArr[(int)PsvType.ammo] - AddAmmo);
        }
    }
    public void On_Finish()
    {
        Enemy.SetActive(false);
        //PlayerUI.SetActive(false);
        FinishUI.SetActive(true);
        LeftLaser.SetActive(true);
        GameObject.FindObjectOfType<OculesPlayer>().isDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerUI == null || PlayerUIPos == null || PlayerPos == null) return;
        PlayerUI.transform.position = new Vector3(PlayerUIPos.position.x, PlayerUIPos.position.y, PlayerUIPos.position.z);
        //Vector3 UIPos = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z) - new Vector3(PlayerUI.transform.position.x, 0, PlayerUI.transform.position.z);
        //Quaternion UIRot = Quaternion.LookRotation(PlayerUI.transform.position);
        //PlayerUI.transform.rotation = UIRot;
        PlayerUI.transform.LookAt(PlayerPos.position);
    }
}
